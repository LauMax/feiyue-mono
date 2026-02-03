import { useEffect, useState } from "react";
import { Users, Sparkles, Clock, XCircle, User } from "lucide-react";
import { Card } from "./ui/card";
import { Button } from "./ui/button";
import type { UserProfileData } from "./UserProfile";

interface WaitingRoomProps {
  userProfile: UserProfileData;
  waitTime?: number;
  onCancel?: () => void;
}

export function WaitingRoom({ userProfile, waitTime = 0, onCancel }: WaitingRoomProps) {
  const [dots, setDots] = useState(".");
  const [elapsedTime, setElapsedTime] = useState(waitTime);
  const [isTooltipVisible, setIsTooltipVisible] = useState(false);

  useEffect(() => {
    const dotsInterval = setInterval(() => {
      setDots(prev => prev.length >= 3 ? "." : prev + ".");
    }, 500);

    return () => clearInterval(dotsInterval);
  }, []);

  // 更新已等待时间
  useEffect(() => {
    const timeInterval = setInterval(() => {
      setElapsedTime(prev => prev + 1);
    }, 1000);

    return () => clearInterval(timeInterval);
  }, []);

  const minutes = Math.floor(elapsedTime / 60);
  const seconds = elapsedTime % 60;
  const genderText = userProfile.gender === "male" ? "男生" : userProfile.gender === "female" ? "女生" : "用户";
  const lookingForText = userProfile.gender === "male" ? "女生" : userProfile.gender === "female" ? "男生" : "伙伴";

  return (
    <div className="min-h-screen flex items-center justify-center p-4 sm:p-6">
      <div className="max-w-md w-full text-center space-y-6 sm:space-y-8">
        <div className="relative">
          <div className="w-20 h-20 sm:w-24 sm:h-24 mx-auto bg-gradient-to-br from-purple-500 to-blue-600 rounded-full flex items-center justify-center shadow-lg shadow-purple-500/50">
            <Users className="w-10 h-10 sm:w-12 sm:h-12 text-white animate-pulse" />
          </div>
          
          {/* Animated circles */}
          <div className="absolute inset-0 flex items-center justify-center">
            <div className="w-28 h-28 sm:w-32 sm:h-32 border-4 border-purple-400/30 rounded-full animate-ping" />
          </div>
        </div>

        <div className="space-y-3 sm:space-y-4">
          <h2 className="text-2xl sm:text-3xl font-bold text-white px-4">
            正在寻找你的故事伙伴{dots}
          </h2>
          <p className="text-purple-200">
            系统正在为你匹配最合适的{lookingForText}
          </p>
        </div>

        {/* Wait Time Display */}
        <div className="bg-gradient-to-r from-purple-500/10 to-blue-500/10 backdrop-blur-lg rounded-2xl p-4 border border-purple-400/30">
          <div className="flex items-center justify-center gap-2 text-purple-300 text-sm">
            <Clock className="w-4 h-4" />
            <span>已等待: {minutes}:{seconds.toString().padStart(2, '0')}</span>
          </div>
        </div>

        {/* User Info */}
        <div className="bg-white/10 backdrop-blur-lg rounded-2xl p-5 sm:p-6 border border-white/20 space-y-3">
          <div className="flex items-center justify-center gap-3">
            <div className="w-12 h-12 bg-gradient-to-br from-pink-500 to-purple-600 rounded-full flex items-center justify-center">
              <User className="w-6 h-6 text-white" />
            </div>
            <div className="text-left">
              <p className="text-purple-300 text-xs">你的身份</p>
              <h3 className="text-lg font-bold text-white">{genderText}</h3>
            </div>
          </div>
          {userProfile.tags && userProfile.tags.length > 0 && (
            <div className="flex flex-wrap justify-center gap-2 pt-2">
              {userProfile.tags.map((tag, index) => (
                <span key={index} className="bg-purple-500/20 text-purple-200 px-3 py-1 rounded-full text-xs">
                  {tag}
                </span>
              ))}
            </div>
          )}
        </div>

        {/* Status Tips */}
        <div className="space-y-2">
          <div className="flex items-center justify-center gap-2 text-sm text-purple-300">
            <Sparkles className="w-4 h-4" />
            <span>匹配成功后将为你们生成专属故事</span>
          </div>
          <div className="flex items-center justify-center gap-2 text-sm text-yellow-300">
            <Clock className="w-4 h-4" />
            <span>正在等待{lookingForText}加入...</span>
          </div>
          {elapsedTime > 300 && (
            <div className="flex items-center justify-center gap-2 text-sm text-purple-300">
              <Sparkles className="w-4 h-4" />
              <span>感谢你的耐心等待</span>
            </div>
          )}
        </div>

        {/* Cancel Button */}
        {onCancel && (
          <Button
            onClick={onCancel}
            variant="outline"
            className="w-full border-red-400/50 text-red-300 hover:bg-red-500/20 hover:text-red-200 gap-2"
          >
            <XCircle className="w-4 h-4" />
            取消匹配
          </Button>
        )}

        {/* Tooltip */}
        <div className="relative">
          <button
            onMouseEnter={() => setIsTooltipVisible(true)}
            onMouseLeave={() => setIsTooltipVisible(false)}
            className="text-purple-400 hover:text-purple-300 text-xs underline"
          >
            为什么要等待？
          </button>
          {isTooltipVisible && (
            <div className="absolute bottom-full left-1/2 transform -translate-x-1/2 mb-2 w-48 bg-black/80 text-white text-xs rounded-lg p-3 whitespace-normal z-10">
              <p>系统正在根据你的偏好从等待队列中寻找最合适的配对对象。通常需要几秒到几分钟。</p>
              <div className="absolute top-full left-1/2 transform -translate-x-1/2 w-2 h-2 bg-black/80 rotate-45" />
            </div>
          )}
        </div>
      </div>
    </div>
  );
}