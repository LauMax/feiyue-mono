import { useState } from "react";
import { Button } from "./ui/button";
import { Input } from "./ui/input";
import { Card } from "./ui/card";
import { Label } from "./ui/label";
import { Badge } from "./ui/badge";
import { Textarea } from "./ui/textarea";
import { User, Tag, X, ArrowRight, Ruler, Weight } from "lucide-react";

export type UserProfileData = {
  gender: "male" | "female" | "other" | null;
  ageGroup: "<18" | "18-23" | "23+" | null;
  height: string;  // 身高，如 "170"
  weight: string;  // 体重，如 "60"
  tags: string[];
  description: string;
};

interface UserProfileProps {
  onComplete: (profile: UserProfileData) => void;
}

const PRESET_TAGS = [
  "Dom", "Sub", "Switch",
  "温柔", "霸道", "傲娇",
  "小清新", "重口", "剧情向",
  "浪漫", "刺激", "慢热",
  "文艺", "野性", "神秘"
];

export function UserProfile({ onComplete }: UserProfileProps) {
  const [gender, setGender] = useState<"male" | "female" | "other" | null>(null);
  const [ageGroup, setAgeGroup] = useState<"<18" | "18-23" | "23+" | null>(null);
  const [height, setHeight] = useState("");
  const [weight, setWeight] = useState("");
  const [tags, setTags] = useState<string[]>([]);
  const [customTag, setCustomTag] = useState("");
  const [description, setDescription] = useState("");

  const handleAddTag = (tag: string) => {
    if (!tags.includes(tag) && tags.length < 8) {
      setTags([...tags, tag]);
    }
  };

  const handleRemoveTag = (tag: string) => {
    setTags(tags.filter(t => t !== tag));
  };

  const handleAddCustomTag = () => {
    if (customTag.trim() && !tags.includes(customTag.trim()) && tags.length < 8) {
      setTags([...tags, customTag.trim()]);
      setCustomTag("");
    }
  };

  const handleSubmit = () => {
    if (gender && ageGroup) {
      onComplete({ gender, ageGroup, height, weight, tags, description });
    }
  };

  const isValid = gender && ageGroup;

  return (
    <div className="min-h-screen flex items-center justify-center p-4 sm:p-6 overflow-y-auto">
      <div className="max-w-2xl w-full space-y-6 my-8">
        {/* Header */}
        <div className="text-center space-y-3">
          <div className="flex justify-center">
            <div className="w-16 h-16 bg-gradient-to-br from-pink-500 to-purple-600 rounded-full flex items-center justify-center shadow-lg shadow-purple-500/50">
              <User className="w-8 h-8 text-white" />
            </div>
          </div>
          <h2 className="text-3xl sm:text-4xl font-bold text-white">
            完善你的资料
          </h2>
          <p className="text-purple-200">
            帮助我们为你匹配更合适的故事伙伴
          </p>
        </div>

        {/* Form */}
        <Card className="bg-white/10 backdrop-blur-lg border-white/20 p-6 sm:p-8 space-y-6">
          {/* Gender Selection */}
          <div className="space-y-3">
            <Label className="text-white text-base">性别 *</Label>
            <div className="grid grid-cols-3 gap-3">
              <Button
                type="button"
                onClick={() => setGender("male")}
                className={`h-12 ${
                  gender === "male"
                    ? "bg-gradient-to-r from-blue-500 to-purple-600 text-white"
                    : "bg-white/10 text-purple-200 hover:bg-white/20"
                }`}
              >
                男生
              </Button>
              <Button
                type="button"
                onClick={() => setGender("female")}
                className={`h-12 ${
                  gender === "female"
                    ? "bg-gradient-to-r from-pink-500 to-purple-600 text-white"
                    : "bg-white/10 text-purple-200 hover:bg-white/20"
                }`}
              >
                女生
              </Button>
              <Button
                type="button"
                onClick={() => setGender("other")}
                className={`h-12 ${
                  gender === "other"
                    ? "bg-gradient-to-r from-purple-500 to-indigo-600 text-white"
                    : "bg-white/10 text-purple-200 hover:bg-white/20"
                }`}
              >
                其他
              </Button>
            </div>
          </div>

          {/* Age Group Selection */}
          <div className="space-y-3">
            <Label className="text-white text-base">年龄段 *</Label>
            <div className="grid grid-cols-3 gap-3">
              <Button
                type="button"
                onClick={() => setAgeGroup("<18")}
                className={`h-12 ${
                  ageGroup === "<18"
                    ? "bg-gradient-to-r from-pink-500 to-purple-600 text-white"
                    : "bg-white/10 text-purple-200 hover:bg-white/20"
                }`}
              >
                &lt;18岁
              </Button>
              <Button
                type="button"
                onClick={() => setAgeGroup("18-23")}
                className={`h-12 ${
                  ageGroup === "18-23"
                    ? "bg-gradient-to-r from-pink-500 to-purple-600 text-white"
                    : "bg-white/10 text-purple-200 hover:bg-white/20"
                }`}
              >
                18-23岁
              </Button>
              <Button
                type="button"
                onClick={() => setAgeGroup("23+")}
                className={`h-12 ${
                  ageGroup === "23+"
                    ? "bg-gradient-to-r from-pink-500 to-purple-600 text-white"
                    : "bg-white/10 text-purple-200 hover:bg-white/20"
                }`}
              >
                23岁以上
              </Button>
            </div>
          </div>

          {/* Height and Weight */}
          <div className="space-y-3">
            <Label className="text-white text-base">身高和体重</Label>
            <div className="flex gap-2">
              <Input
                value={height}
                onChange={(e) => setHeight(e.target.value)}
                placeholder="身高 (cm)..."
                maxLength={3}
                className="flex-1 bg-white/10 border-white/20 text-white placeholder:text-purple-300 focus:border-purple-400"
              />
              <Ruler className="w-4 h-4 text-purple-200" />
              <Input
                value={weight}
                onChange={(e) => setWeight(e.target.value)}
                placeholder="体重 (kg)..."
                maxLength={3}
                className="flex-1 bg-white/10 border-white/20 text-white placeholder:text-purple-300 focus:border-purple-400"
              />
              <Weight className="w-4 h-4 text-purple-200" />
            </div>
          </div>

          {/* Tags Selection */}
          <div className="space-y-3">
            <div className="flex items-center justify-between">
              <Label className="text-white text-base">偏好标签</Label>
              <span className="text-purple-300 text-sm">{tags.length}/8</span>
            </div>
            
            {/* Selected Tags */}
            {tags.length > 0 && (
              <div className="flex flex-wrap gap-2 p-3 bg-black/20 rounded-lg border border-white/10">
                {tags.map((tag) => (
                  <Badge
                    key={tag}
                    className="bg-gradient-to-r from-pink-500 to-purple-600 text-white pl-3 pr-1 py-1.5 text-sm"
                  >
                    {tag}
                    <button
                      onClick={() => handleRemoveTag(tag)}
                      className="ml-1.5 hover:bg-white/20 rounded-full p-0.5"
                    >
                      <X className="w-3 h-3" />
                    </button>
                  </Badge>
                ))}
              </div>
            )}

            {/* Preset Tags */}
            <div className="flex flex-wrap gap-2">
              {PRESET_TAGS.map((tag) => (
                <Badge
                  key={tag}
                  onClick={() => handleAddTag(tag)}
                  className={`cursor-pointer transition-all ${
                    tags.includes(tag)
                      ? "bg-white/20 text-purple-300 opacity-50 cursor-not-allowed"
                      : "bg-white/10 text-purple-200 hover:bg-white/20"
                  }`}
                >
                  {tag}
                </Badge>
              ))}
            </div>

            {/* Custom Tag Input */}
            <div className="flex gap-2">
              <Input
                value={customTag}
                onChange={(e) => setCustomTag(e.target.value)}
                onKeyPress={(e) => e.key === "Enter" && handleAddCustomTag()}
                placeholder="自定义标签..."
                maxLength={10}
                disabled={tags.length >= 8}
                className="flex-1 bg-white/10 border-white/20 text-white placeholder:text-purple-300 focus:border-purple-400"
              />
              <Button
                type="button"
                onClick={handleAddCustomTag}
                disabled={!customTag.trim() || tags.length >= 8}
                className="bg-purple-500/20 text-purple-200 hover:bg-purple-500/30"
              >
                <Tag className="w-4 h-4" />
              </Button>
            </div>
          </div>

          {/* Description */}
          <div className="space-y-3">
            <Label className="text-white text-base">个人描述</Label>
            <Textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="简单介绍一下自己的喜好和期待..."
              maxLength={200}
              className="min-h-[100px] bg-white/10 border-white/20 text-white placeholder:text-purple-300 focus:border-purple-400 resize-none"
            />
            <div className="text-right text-purple-300 text-sm">
              {description.length}/200
            </div>
          </div>
        </Card>

        {/* Submit Button */}
        <Button
          onClick={handleSubmit}
          disabled={!isValid}
          className="w-full bg-gradient-to-r from-pink-500 to-purple-600 hover:from-pink-600 hover:to-purple-700 text-white h-12 disabled:opacity-50 disabled:cursor-not-allowed"
        >
          开始匹配
          <ArrowRight className="w-5 h-5 ml-2" />
        </Button>

        <p className="text-center text-purple-300 text-sm">
          你的信息将仅用于匹配，完全匿名
        </p>
      </div>
    </div>
  );
}