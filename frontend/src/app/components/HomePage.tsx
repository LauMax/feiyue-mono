import { Button } from "./ui/button";
import { Sparkles, Users, MessageCircle } from "lucide-react";

interface HomePageProps {
  onStart: () => void;
}

export function HomePage({ onStart }: HomePageProps) {
  return (
    <div className="min-h-screen flex items-center justify-center p-4 sm:p-6">
      <div className="max-w-4xl w-full text-center space-y-6 sm:space-y-8">
        {/* Logo & Title */}
        <div className="space-y-3 sm:space-y-4">
          <div className="flex justify-center">
            <div className="w-16 h-16 sm:w-20 sm:h-20 bg-gradient-to-br from-pink-500 to-purple-600 rounded-full flex items-center justify-center shadow-lg shadow-purple-500/50">
              <MessageCircle className="w-8 h-8 sm:w-10 sm:h-10 text-white" />
            </div>
          </div>
          <h1 className="text-5xl sm:text-6xl font-bold bg-gradient-to-r from-pink-400 via-purple-400 to-pink-400 bg-clip-text text-transparent">
            绯悦
          </h1>
          <p className="text-lg sm:text-xl text-purple-200 px-4">
            AI驱动的匿名角色扮演聊天
          </p>
        </div>

        {/* Features */}
        <div className="grid grid-cols-1 sm:grid-cols-3 gap-4 sm:gap-6 mt-8 sm:mt-12">
          <div className="bg-white/10 backdrop-blur-lg rounded-2xl p-5 sm:p-6 border border-white/20">
            <div className="w-12 h-12 bg-purple-500/20 rounded-xl flex items-center justify-center mx-auto mb-3 sm:mb-4">
              <Sparkles className="w-6 h-6 text-purple-300" />
            </div>
            <h3 className="font-semibold text-white mb-2">AI生成故事</h3>
            <p className="text-purple-200 text-sm">
              由AI创造独特的故事背景，每次都是全新的冒险
            </p>
          </div>

          <div className="bg-white/10 backdrop-blur-lg rounded-2xl p-5 sm:p-6 border border-white/20">
            <div className="w-12 h-12 bg-pink-500/20 rounded-xl flex items-center justify-center mx-auto mb-3 sm:mb-4">
              <Users className="w-6 h-6 text-pink-300" />
            </div>
            <h3 className="font-semibold text-white mb-2">角色扮演</h3>
            <p className="text-purple-200 text-sm">
              选择你的角色，与陌生人一起演绎精彩故事
            </p>
          </div>

          <div className="bg-white/10 backdrop-blur-lg rounded-2xl p-5 sm:p-6 border border-white/20">
            <div className="w-12 h-12 bg-purple-500/20 rounded-xl flex items-center justify-center mx-auto mb-3 sm:mb-4">
              <MessageCircle className="w-6 h-6 text-purple-300" />
            </div>
            <h3 className="font-semibold text-white mb-2">完全匿名</h3>
            <p className="text-purple-200 text-sm">
              无需注册，自由表达，享受纯粹的故事体验
            </p>
          </div>
        </div>

        {/* CTA */}
        <div className="mt-8 sm:mt-12">
          <Button 
            onClick={onStart}
            className="bg-gradient-to-r from-pink-500 to-purple-600 hover:from-pink-600 hover:to-purple-700 text-white px-10 sm:px-12 py-5 sm:py-6 rounded-full shadow-lg shadow-purple-500/50 transition-all hover:scale-105 w-full sm:w-auto"
          >
            开始你的故事
          </Button>
        </div>

        {/* Footer */}
        <div className="mt-8 sm:mt-12 text-purple-300 text-sm">
          <p>遇见陌生人，创造难忘的故事</p>
        </div>
      </div>
    </div>
  );
}