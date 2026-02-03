import { useState, useRef, useEffect } from "react";
import { Button } from "./ui/button";
import { Input } from "./ui/input";
import { Card } from "./ui/card";
import { Send, LogOut, User, Sparkles, Info, Lightbulb } from "lucide-react";
import { Sheet, SheetContent, SheetHeader, SheetTitle, SheetTrigger } from "./ui/sheet";
import type { Story } from "../App";
import type { UserProfileData } from "./UserProfile";
import api from "../../api";

interface ChatRoomProps {
  story: Story;
  role: "A" | "B";
  userProfile: UserProfileData;  // 自己的资料
  partnerProfile: UserProfileData;  // 对方的资料
  onExit: () => void;
  userId: string;
  roomId: string;
}

type Message = {
  id: string;
  sender: "A" | "B" | "system";
  content: string;
  timestamp: Date;
  isStoryClue?: boolean;
};

export function ChatRoom({ story, role, userProfile, partnerProfile, onExit, userId, roomId }: ChatRoomProps) {
  // 默认故事背景模板（当后端返回的背景不完整时使用）
  const DEFAULT_BACKGROUNDS = [
    "在繁华都市的一角，有一家充满温馨氛围的咖啡馆。午后的阳光透过落地窗洒落，空气中弥漫着咖啡的香气。命运在这里悄然安排了一场相遇...",
    "夜幕降临，城市的霓虹灯开始闪烁。在这个充满可能性的夜晚，两个陌生的灵魂即将相遇...",
    "校园的樱花树下，微风轻拂。阳光透过花瓣的缝隙洒落，勾勒出一个浪漫的场景...",
    "雨后的小镇，空气中弥漫着泥土的清香。一把红伞下，命运的齿轮开始转动...",
    "深夜的酒吧，昏暗的灯光下，两道目光悄然交汇。这一刻，时间仿佛静止了..."
  ];

  // 检查故事背景是否有效（不是用户描述被错误使用）
  const isValidBackground = (bg: string): boolean => {
    if (!bg || bg.length < 30) return false;
    // 如果背景与任何用户描述相同，说明后端错误使用了用户描述
    if (bg === userProfile.description || bg === partnerProfile.description) return false;
    return true;
  };

  // 获取有效的故事背景
  const getStoryBackground = (): string => {
    if (isValidBackground(story.background)) {
      return story.background;
    }
    // 使用随机默认背景
    return DEFAULT_BACKGROUNDS[Math.floor(Math.random() * DEFAULT_BACKGROUNDS.length)];
  };

  // 简化的故事种子生成（仅用于初始显示）
  const storySeed = getStoryBackground();

  const [messages, setMessages] = useState<Message[]>([
    // 故事种子 - 第一条系统消息
    {
      id: "story-seed",
      sender: "system",
      content: storySeed,
      timestamp: new Date(),
      isStoryClue: true
    }
  ]);
  const [inputValue, setInputValue] = useState("");
  const [isSending, setIsSending] = useState(false);
  const messagesEndRef = useRef<HTMLDivElement>(null);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  useEffect(() => {
    scrollToBottom();
  }, [messages]);

  // 初始化时加载聊天历史
  useEffect(() => {
    const loadMessages = async () => {
      if (!roomId) return;
      
      try {
        const result = await api.chatMessages(roomId);
        const serverMessages = Array.isArray(result.data) ? result.data : (result.data?.messages || []);
        
        if (result.success && serverMessages.length > 0) {
          console.log("[初始化] 加载聊天历史:", serverMessages.length, "条消息");
          
          // 筛选出除故事种子外的历史消息
          const historyMessages = serverMessages
            .filter(msg => msg.id !== "story-seed")
            .map(msg => ({
              id: msg.id,
              sender: msg.role,
              content: msg.message,
              timestamp: new Date(msg.timestamp),
              isStoryClue: msg.isStoryClue || false
            }));
          
          if (historyMessages.length > 0) {
            setMessages(prev => [...prev, ...historyMessages]);
          }
        }
      } catch (error) {
        console.error("加载聊天历史失败:", error);
      }
    };

    loadMessages();
  }, [roomId]);

  // 轮询获取新消息（只获取对方消息和系统剧情线索，不获取自己的消息避免重复）
  useEffect(() => {
    if (!roomId) return;

    const pollMessages = async () => {
      try {
        const result = await api.chatMessages(roomId);
        if (result.success && result.data) {
          const serverMessages = Array.isArray(result.data) ? result.data : result.data.messages;
          
          // 筛选新消息：只接收对方消息和系统消息，不接收自己的消息（避免与乐观更新重复）
          const existingIds = new Set(messages.map(m => m.id));
          const partnerRole = role === "A" ? "B" : "A";
          const newMessages = serverMessages.filter(msg => 
            !existingIds.has(msg.id) && 
            (msg.role === partnerRole || msg.role === 'system')
          );
          
          if (newMessages.length > 0) {
            const formattedMessages = newMessages.map(msg => ({
              id: msg.id,
              sender: msg.role,
              content: msg.message,
              timestamp: new Date(msg.timestamp),
              isStoryClue: msg.isStoryClue || false
            }));
            
            setMessages(prev => [...prev, ...formattedMessages]);
          }
        }
      } catch (error) {
        console.error("轮询消息失败:", error);
      }
    };

    const pollInterval = setInterval(pollMessages, 2000);
    return () => clearInterval(pollInterval);
  }, [roomId, messages, role]);



  const handleSend = async () => {
    if (!inputValue.trim() || isSending) return;

    setIsSending(true);
    const messageText = inputValue;
    
    // 立即添加到本地 (乐观更新)
    const newMessage: Message = {
      id: Date.now().toString(),
      sender: role,
      content: messageText,
      timestamp: new Date()
    };
    setMessages(prev => [...prev, newMessage]);
    setInputValue("");

    try {
      // 调用真实 API 发送消息
      const result = await api.chatSend({
        roomId,
        userId,
        message: messageText,
      });

      if (result.success) {
        // 如果后端返回了剧情线索，添加到消息列表
        if (result.data?.storyClue) {
          const clueMessage: Message = {
            id: result.data.storyClue.id,
            sender: "system",
            content: result.data.storyClue.message,
            timestamp: new Date(result.data.storyClue.timestamp),
            isStoryClue: true
          };
          setMessages(prev => [...prev, clueMessage]);
        }
      } else {
        console.error("发送消息失败:", result.error);
        // 移除失败的乐观更新
        setMessages(prev => prev.filter(m => m.id !== newMessage.id));
        alert("发送消息失败，请重试");
      }
    } catch (error) {
      console.error("发送消息出错:", error);
      // 移除失败的乐观更新
      setMessages(prev => prev.filter(m => m.id !== newMessage.id));
    } finally {
      setIsSending(false);
    }
  };

  const handleKeyPress = (e: React.KeyboardEvent) => {
    if (e.key === "Enter" && !e.shiftKey) {
      e.preventDefault();
      handleSend();
    }
  };

  const currentRole = role === "A" ? story.maleRole : story.femaleRole;
  const otherRole = role === "A" ? story.femaleRole : story.maleRole;

  return (
    <div className="h-screen flex flex-col">
      {/* Header */}
      <div className="bg-white/10 backdrop-blur-lg border-b border-white/20 p-3 sm:p-4">
        <div className="flex items-center justify-between gap-2">
          <div className="flex items-center gap-2 sm:gap-3 min-w-0 flex-1">
            <div className="bg-gradient-to-br from-purple-500 to-blue-600 rounded-full p-1.5 sm:p-2 flex-shrink-0">
              <Sparkles className="w-4 h-4 sm:w-5 sm:h-5 text-white" />
            </div>
            <div className="min-w-0 flex-1">
              <h3 className="text-white font-semibold text-sm sm:text-base truncate">{story.title}</h3>
              <p className="text-purple-300 text-xs sm:text-sm truncate">与 {otherRole.name} 对话中</p>
            </div>
          </div>
          
          <div className="flex items-center gap-2 flex-shrink-0">
            {/* Mobile Info Sheet */}
            <Sheet>
              <SheetTrigger asChild>
                <Button
                  variant="ghost"
                  size="sm"
                  className="lg:hidden text-purple-300 hover:text-white p-2"
                >
                  <Info className="w-5 h-5" />
                </Button>
              </SheetTrigger>
              <SheetContent side="right" className="bg-slate-900 border-white/20 text-white w-[85vw] sm:w-[400px] flex flex-col h-full overflow-hidden">
                <SheetHeader className="flex-shrink-0">
                  <SheetTitle className="text-white">故事信息</SheetTitle>
                </SheetHeader>
                <div className="mt-6 space-y-4 overflow-y-auto flex-1 pr-2 pb-6 overscroll-contain" style={{ WebkitOverflowScrolling: 'touch' }}>
                  {/* Your Role */}
                  <Card className="bg-white/10 backdrop-blur-lg border-white/20 p-4">
                    <div className="flex items-center gap-3 mb-3">
                      <div className="w-10 h-10 bg-gradient-to-br from-pink-500 to-purple-600 rounded-full flex items-center justify-center">
                        <User className="w-5 h-5 text-white" />
                      </div>
                      <div>
                        <p className="text-purple-300 text-xs">你的角色</p>
                        <h4 className="text-white font-semibold">{currentRole.name}</h4>
                      </div>
                    </div>
                    <p className="text-purple-200 text-sm mb-2">{currentRole.description}</p>
                    <div className="bg-black/20 rounded-lg p-2 border border-white/10">
                      <p className="text-xs text-purple-300">{currentRole.personality}</p>
                    </div>
                  </Card>

                  {/* Other Role */}
                  <Card className="bg-white/10 backdrop-blur-lg border-white/20 p-4">
                    <div className="flex items-center gap-3 mb-3">
                      <div className="w-10 h-10 bg-gradient-to-br from-purple-500 to-blue-600 rounded-full flex items-center justify-center">
                        <User className="w-5 h-5 text-white" />
                      </div>
                      <div>
                        <p className="text-purple-300 text-xs">对方角色</p>
                        <h4 className="text-white font-semibold">{otherRole.name}</h4>
                      </div>
                    </div>
                    <p className="text-purple-200 text-sm mb-2">{otherRole.description}</p>
                    <div className="bg-black/20 rounded-lg p-2 border border-white/10">
                      <p className="text-xs text-purple-300">{otherRole.personality}</p>
                    </div>
                  </Card>

                  {/* Partner Info */}
                  <Card className="bg-white/10 backdrop-blur-lg border-white/20 p-4">
                    <h4 className="text-white font-semibold mb-3 text-sm">对方信息</h4>
                    <div className="space-y-2 text-xs">
                      <div className="flex justify-between items-center">
                        <span className="text-purple-300">性别</span>
                        <span className="text-white">
                          {partnerProfile.gender === "male" ? "男生" : partnerProfile.gender === "female" ? "女生" : "其他"}
                        </span>
                      </div>
                      <div className="flex justify-between items-center">
                        <span className="text-purple-300">年龄段</span>
                        <span className="text-white">{partnerProfile.ageGroup}</span>
                      </div>
                      {partnerProfile.height && (
                        <div className="flex justify-between items-center">
                          <span className="text-purple-300">身高</span>
                          <span className="text-white">{partnerProfile.height} cm</span>
                        </div>
                      )}
                      {partnerProfile.weight && (
                        <div className="flex justify-between items-center">
                          <span className="text-purple-300">体重</span>
                          <span className="text-white">{partnerProfile.weight} kg</span>
                        </div>
                      )}
                      {partnerProfile.tags && partnerProfile.tags.length > 0 && (
                        <div className="pt-2 border-t border-white/10">
                          <p className="text-purple-300 mb-2">标签</p>
                          <div className="flex flex-wrap gap-1">
                            {partnerProfile.tags.map((tag, index) => (
                              <span key={index} className="bg-purple-500/20 text-purple-200 px-2 py-0.5 rounded-full text-xs">
                                {tag}
                              </span>
                            ))}
                          </div>
                        </div>
                      )}
                      {partnerProfile.description && (
                        <div className="pt-2 border-t border-white/10">
                          <p className="text-purple-300 mb-1">个人描述</p>
                          <p className="text-purple-200 leading-relaxed">{partnerProfile.description}</p>
                        </div>
                      )}
                    </div>
                  </Card>

                  {/* Initial Story Seed */}
                  <Card className="bg-gradient-to-br from-purple-500/10 to-pink-500/10 backdrop-blur-lg border-purple-400/30 p-4">
                    <div className="flex items-center gap-2 mb-2">
                      <Sparkles className="w-4 h-4 text-yellow-400" />
                      <h4 className="text-white font-semibold text-sm">初始故事</h4>
                    </div>
                    <p className="text-purple-100 text-xs leading-relaxed italic">{storySeed}</p>
                  </Card>

                  {/* Story Background */}
                  <Card className="bg-white/10 backdrop-blur-lg border-white/20 p-4">
                    <h4 className="text-white font-semibold mb-2 text-sm">故事背景</h4>
                    <p className="text-purple-200 text-xs leading-relaxed">{story.background}</p>
                  </Card>
                </div>
              </SheetContent>
            </Sheet>

            <Button
              onClick={onExit}
              variant="ghost"
              size="sm"
              className="text-purple-300 hover:text-white p-2"
            >
              <LogOut className="w-4 h-4 sm:w-5 sm:h-5" />
              <span className="hidden sm:inline ml-2">离开</span>
            </Button>
          </div>
        </div>
      </div>

      {/* Main Chat Area */}
      <div className="flex-1 flex gap-4 max-w-7xl w-full mx-auto overflow-hidden p-0 sm:p-4">
        {/* Sidebar - Desktop Only */}
        <div className="hidden lg:flex lg:flex-col w-64 space-y-4 flex-shrink-0 overflow-y-auto overscroll-contain pr-2" style={{ WebkitOverflowScrolling: 'touch' }}>
          {/* Your Role */}
          <Card className="bg-white/10 backdrop-blur-lg border-white/20 p-4">
            <div className="flex items-center gap-3 mb-3">
              <div className="w-10 h-10 bg-gradient-to-br from-pink-500 to-purple-600 rounded-full flex items-center justify-center">
                <User className="w-5 h-5 text-white" />
              </div>
              <div>
                <p className="text-purple-300 text-xs">你的角色</p>
                <h4 className="text-white font-semibold">{currentRole.name}</h4>
              </div>
            </div>
            <p className="text-purple-200 text-sm mb-2">{currentRole.description}</p>
            <div className="bg-black/20 rounded-lg p-2 border border-white/10">
              <p className="text-xs text-purple-300">{currentRole.personality}</p>
            </div>
          </Card>

          {/* Other Role */}
          <Card className="bg-white/10 backdrop-blur-lg border-white/20 p-4">
            <div className="flex items-center gap-3 mb-3">
              <div className="w-10 h-10 bg-gradient-to-br from-purple-500 to-blue-600 rounded-full flex items-center justify-center">
                <User className="w-5 h-5 text-white" />
              </div>
              <div>
                <p className="text-purple-300 text-xs">对方角色</p>
                <h4 className="text-white font-semibold">{otherRole.name}</h4>
              </div>
            </div>
            <p className="text-purple-200 text-sm mb-2">{otherRole.description}</p>
            <div className="bg-black/20 rounded-lg p-2 border border-white/10">
              <p className="text-xs text-purple-300">{otherRole.personality}</p>
            </div>
          </Card>

          {/* Partner Info */}
          <Card className="bg-white/10 backdrop-blur-lg border-white/20 p-4">
            <h4 className="text-white font-semibold mb-3 text-sm">对方信息</h4>
            <div className="space-y-2 text-xs">
              <div className="flex justify-between items-center">
                <span className="text-purple-300">性别</span>
                <span className="text-white">
                  {partnerProfile.gender === "male" ? "男生" : partnerProfile.gender === "female" ? "女生" : "其他"}
                </span>
              </div>
              <div className="flex justify-between items-center">
                <span className="text-purple-300">年龄段</span>
                <span className="text-white">{partnerProfile.ageGroup}</span>
              </div>
              {partnerProfile.height && (
                <div className="flex justify-between items-center">
                  <span className="text-purple-300">身高</span>
                  <span className="text-white">{partnerProfile.height} cm</span>
                </div>
              )}
              {partnerProfile.weight && (
                <div className="flex justify-between items-center">
                  <span className="text-purple-300">体重</span>
                  <span className="text-white">{partnerProfile.weight} kg</span>
                </div>
              )}
              {partnerProfile.tags && partnerProfile.tags.length > 0 && (
                <div className="pt-2 border-t border-white/10">
                  <p className="text-purple-300 mb-2">标签</p>
                  <div className="flex flex-wrap gap-1">
                    {partnerProfile.tags.map((tag, index) => (
                      <span key={index} className="bg-purple-500/20 text-purple-200 px-2 py-0.5 rounded-full text-xs">
                        {tag}
                      </span>
                    ))}
                  </div>
                </div>
              )}
              {partnerProfile.description && (
                <div className="pt-2 border-t border-white/10">
                  <p className="text-purple-300 mb-1">个人描述</p>
                  <p className="text-purple-200 leading-relaxed">{partnerProfile.description}</p>
                </div>
              )}
            </div>
          </Card>

          {/* Initial Story Seed */}
          <Card className="bg-gradient-to-br from-purple-500/10 to-pink-500/10 backdrop-blur-lg border-purple-400/30 p-4">
            <div className="flex items-center gap-2 mb-2">
              <Sparkles className="w-4 h-4 text-yellow-400" />
              <h4 className="text-white font-semibold text-sm">初始故事</h4>
            </div>
            <p className="text-purple-100 text-xs leading-relaxed italic">{storySeed}</p>
          </Card>

          {/* Story Background */}
          <Card className="bg-white/10 backdrop-blur-lg border-white/20 p-4">
            <h4 className="text-white font-semibold mb-2 text-sm">故事背景</h4>
            <p className="text-purple-200 text-xs leading-relaxed">{story.background}</p>
          </Card>
        </div>

        {/* Chat Messages */}
        <div className="flex-1 flex flex-col min-w-0">
          <div className="flex-1 bg-white/5 backdrop-blur-lg border-white/20 sm:rounded-2xl border-0 sm:border p-3 sm:p-4 overflow-hidden flex flex-col">
            <div className="flex-1 overflow-y-auto space-y-3 sm:space-y-4">
              {messages.map((message) => {
                // 系统消息（故事线索）
                if (message.sender === "system") {
                  return (
                    <div key={message.id} className="flex justify-center">
                      <div className="bg-gradient-to-r from-purple-500/20 to-pink-500/20 backdrop-blur-lg border border-purple-400/30 rounded-2xl px-4 py-3 max-w-[85%] sm:max-w-[70%]">
                        <div className="flex items-center gap-2 mb-1">
                          <Lightbulb className="w-4 h-4 text-yellow-400" />
                          <span className="text-purple-200 text-xs font-semibold">故事线索</span>
                        </div>
                        <p className="text-purple-100 text-sm italic leading-relaxed">{message.content}</p>
                      </div>
                    </div>
                  );
                }
                
                // 正常用户消息
                const isMe = message.sender === role;
                const senderRole = message.sender === "A" ? story.maleRole : story.femaleRole;
                
                return (
                  <div
                    key={message.id}
                    className={`flex gap-2 sm:gap-3 ${isMe ? "flex-row-reverse" : "flex-row"}`}
                  >
                    <div className={`w-7 h-7 sm:w-8 sm:h-8 rounded-full flex items-center justify-center flex-shrink-0 ${
                      isMe 
                        ? "bg-gradient-to-br from-pink-500 to-purple-600" 
                        : "bg-gradient-to-br from-purple-500 to-blue-600"
                    }`}>
                      <User className="w-3.5 h-3.5 sm:w-4 sm:h-4 text-white" />
                    </div>
                    <div className={`flex flex-col ${isMe ? "items-end" : "items-start"} max-w-[75%] sm:max-w-[70%]`}>
                      <div className="flex items-center gap-2 mb-1">
                        <span className="text-purple-300 text-xs">{senderRole.name}</span>
                      </div>
                      <div className={`rounded-2xl px-3 py-2 sm:px-4 sm:py-2 ${
                        isMe 
                          ? "bg-gradient-to-r from-pink-500 to-purple-600 text-white" 
                          : "bg-white/10 text-white border border-white/20"
                      }`}>
                        <p className="text-sm break-words">{message.content}</p>
                      </div>
                    </div>
                  </div>
                );
              })}
              <div ref={messagesEndRef} />
            </div>
          </div>

          {/* Input Area */}
          <div className="p-3 sm:p-0 sm:pt-4 flex gap-2 bg-gradient-to-t from-slate-900 via-slate-900 to-transparent sm:bg-none">
            <Input
              value={inputValue}
              onChange={(e) => setInputValue(e.target.value)}
              onKeyPress={handleKeyPress}
              placeholder="输入你的消息..."
              disabled={isSending}
              className="flex-1 bg-white/10 border-white/20 text-white placeholder:text-purple-300 focus:border-purple-400 h-11"
            />
            <Button
              onClick={handleSend}
              disabled={isSending || !inputValue.trim()}
              className="bg-gradient-to-r from-pink-500 to-purple-600 hover:from-pink-600 hover:to-purple-700 text-white h-11 w-11 p-0 flex-shrink-0 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              {isSending ? (
                <div className="w-5 h-5 border-2 border-white border-t-transparent rounded-full animate-spin" />
              ) : (
                <Send className="w-5 h-5" />
              )}
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
}