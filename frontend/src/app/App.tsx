import { useState, useCallback } from "react";
import { HomePage } from "./components/HomePage";
import { UserProfile, UserProfileData } from "./components/UserProfile";
import { WaitingRoom } from "./components/WaitingRoom";
import { ChatRoom } from "./components/ChatRoom";
import api from "../api";

export type Story = {
  title: string;
  background: string;
  maleRole: {
    name: string;
    description: string;
    personality: string;
  };
  femaleRole: {
    name: string;
    description: string;
    personality: string;
  };
};

type AppState = "home" | "profile" | "waiting" | "chatting";

export interface AppContextData {
  userId: string | null;
  matchId: string | null;
  roomId: string | null;
}

export default function App() {
  const [state, setState] = useState<AppState>("home");
  const [userProfile, setUserProfile] = useState<UserProfileData | null>(null);
  const [story, setStory] = useState<Story | null>(null);
  const [selectedRole, setSelectedRole] = useState<"A" | "B" | null>(null);
  const [partnerProfile, setPartnerProfile] = useState<UserProfileData | null>(null);
  const [appData, setAppData] = useState<AppContextData>({
    userId: null,
    matchId: null,
    roomId: null,
  });
  const [waitTime, setWaitTime] = useState(0);
  const [pollIntervalRef, setPollIntervalRef] = useState<NodeJS.Timeout | null>(null);

  const handleStartJourney = () => {
    setState("profile");
  };

  // 资料填写完成后，直接开始匹配
  const handleProfileComplete = useCallback(async (profile: UserProfileData) => {
    setUserProfile(profile);
    setState("waiting");
    setWaitTime(0);

    // 根据性别自动分配角色
    const userRole: "A" | "B" = profile.gender === "male" ? "A" : "B";
    setSelectedRole(userRole);

    try {
      // 发送用户资料和角色进行匹配
      const matchResult = await api.matchRequest({
        profile: profile,
        selectedRole: userRole,
      });

      if (matchResult.success && matchResult.data) {
        const { userId, matchId, status, roomId, story: matchedStory, yourRole, partnerProfile: matchedPartner } = matchResult.data;
        
        setAppData({
          userId,
          matchId,
          roomId: roomId || null,
        });

        // 检查是否直接匹配成功
        if (status === 'matched' && roomId && yourRole) {
          const validStory = isValidStory(matchedStory) ? matchedStory : createDefaultStory();
          setStory(validStory);
          setSelectedRole(yourRole);
          setPartnerProfile(matchedPartner || createMockPartnerProfile(profile.gender));
          setState("chatting");
          return;
        }

        // 开始轮询匹配状态
        let waitTimeCounter = 0;

        const interval = setInterval(async () => {
          waitTimeCounter++;
          setWaitTime(waitTimeCounter);

          try {
            const statusResult = await api.matchStatus(matchId);

            if (statusResult.success && statusResult.data) {
              const statusData = statusResult.data;
              
              // 匹配成功
              if (statusData.status === "matched" && statusData.roomId && statusData.yourRole) {
                clearInterval(interval);
                setPollIntervalRef(null);

                setAppData((prev) => ({
                  ...prev,
                  roomId: statusData.roomId || null,
                }));

                const validStory = isValidStory(statusData.story) ? statusData.story : createDefaultStory();
                setStory(validStory);
                setSelectedRole(statusData.yourRole);
                setPartnerProfile(statusData.partnerProfile || createMockPartnerProfile(profile.gender));
                setState("chatting");
              } else {
                console.log(`[匹配进行中] 已等待 ${waitTimeCounter} 秒...`);
              }
            } else {
              console.warn("查询匹配状态失败，继续轮询:", statusResult.error);
            }
          } catch (error) {
            console.error("轮询出错:", error);
          }
        }, 2000);

        setPollIntervalRef(interval);
      } else {
        console.error("匹配请求失败:", matchResult.error?.message);
        setState("profile");
        setWaitTime(0);
      }
    } catch (error) {
      console.error("匹配出错:", error);
      setState("profile");
      setWaitTime(0);
    }
  }, []);

  // 创建模拟对方资料（后备方案）
  const createMockPartnerProfile = (myGender: string): UserProfileData => ({
    gender: myGender === "male" ? "female" : "male",
    ageGroup: ["<18", "18-23", "23+"][Math.floor(Math.random() * 3)] as "<18" | "18-23" | "23+",
    height: String(Math.floor(Math.random() * 30) + 160),
    weight: String(Math.floor(Math.random() * 30) + 50),
    tags: ["温柔", "浪漫", "小清新"].slice(0, Math.floor(Math.random() * 3) + 1),
    description: "希望能在这段故事中找到共鸣...",
  });

  // 创建默认故事（后备方案，当后端返回空故事时使用）
  const createDefaultStory = (): Story => ({
    title: "命运的邂逅",
    background: "在繁华都市的一角，有一家充满温馨氛围的咖啡馆。午后的阳光透过落地窗洒落，空气中弥漫着咖啡的香气。命运在这里悄然安排了一场相遇...",
    maleRole: {
      name: "神秘旅人",
      description: "一位带着故事的旅人，眼中藏着远方的风景",
      personality: "沉稳而有趣，善于倾听，偶尔会说出令人意想不到的话语",
    },
    femaleRole: {
      name: "咖啡馆女孩",
      description: "喜欢在咖啡馆度过午后时光的女孩",
      personality: "温柔细腻，对生活充满好奇，喜欢发现平凡中的美好",
    },
  });

  // 验证故事对象是否完整
  const isValidStory = (s: any): s is Story => {
    return s && 
      typeof s.title === 'string' && s.title.length > 0 &&
      typeof s.background === 'string' &&
      s.maleRole && typeof s.maleRole.name === 'string' &&
      s.femaleRole && typeof s.femaleRole.name === 'string';
  };

  const handleCancelMatch = useCallback(async () => {
    if (!appData.matchId) return;

    // 清理轮询
    if (pollIntervalRef) {
      clearInterval(pollIntervalRef);
      setPollIntervalRef(null);
    }

    try {
      await api.matchCancel(appData.matchId);
      console.log("已取消匹配");
    } catch (error) {
      console.error("取消匹配失败:", error);
    }

    // 返回资料填写页面
    setState("profile");
    setWaitTime(0);
    setAppData({
      userId: null,
      matchId: null,
      roomId: null,
    });
  }, [appData.matchId, pollIntervalRef]);

  const handleBackToHome = async () => {
    // 清理轮询
    if (pollIntervalRef) {
      clearInterval(pollIntervalRef);
      setPollIntervalRef(null);
    }

    // 如果有房间ID，通知后端离开房间
    if (appData.roomId && appData.userId) {
      try {
        await api.roomLeave(appData.roomId, appData.userId);
      } catch (error) {
        console.error("离开房间出错:", error);
      }
    }

    setState("home");
    setUserProfile(null);
    setStory(null);
    setSelectedRole(null);
    setPartnerProfile(null);
    setAppData({
      userId: null,
      matchId: null,
      roomId: null,
    });
  };

  return (
    <div
      className={`min-h-screen bg-gradient-to-br from-slate-900 via-purple-900 to-slate-900 ${
        state === "chatting" ? "h-screen overflow-hidden" : ""
      }`}
    >
      {state === "home" && <HomePage onStart={handleStartJourney} />}
      {state === "profile" && (
        <UserProfile onComplete={handleProfileComplete} />
      )}
      {state === "waiting" && userProfile && (
        <WaitingRoom 
          userProfile={userProfile}
          waitTime={waitTime}
          onCancel={handleCancelMatch}
        />
      )}
      {state === "chatting" && story && selectedRole && userProfile && partnerProfile && (
        <ChatRoom
          story={story}
          role={selectedRole}
          userProfile={userProfile}
          partnerProfile={partnerProfile}
          onExit={handleBackToHome}
          userId={appData.userId || ""}
          roomId={appData.roomId || ""}
        />
      )}
    </div>
  );
}