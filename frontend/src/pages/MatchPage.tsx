import { useQuery, useMutation } from '@tanstack/react-query'
import { useState } from 'react'
import { matchApi, type MatchRequest, type MatchResponse } from '@/services/matchApi'

export function MatchPage() {
  const [userId] = useState(() => `user-${Date.now()}`)
  const [formData, setFormData] = useState({
    gender: 'male' as 'male' | 'female',
    ageGroup: '18-23',
    tags: [] as string[],
  })
  const [isMatching, setIsMatching] = useState(false)

  // 查询匹配状态
  const { data: matchStatus } = useQuery({
    queryKey: ['matchStatus', userId],
    queryFn: () => matchApi.getMatchStatus(userId),
    enabled: isMatching,
    refetchInterval: isMatching ? 2000 : false, // 每2秒轮询
  })

  // 请求匹配
  const requestMatchMutation = useMutation({
    mutationFn: (request: MatchRequest) => matchApi.requestMatch(request),
    onSuccess: (data: MatchResponse) => {
      if (data.success) {
        setIsMatching(true)
      }
    },
  })

  // 取消匹配
  const cancelMatchMutation = useMutation({
    mutationFn: () => matchApi.cancelMatch(userId),
    onSuccess: () => {
      setIsMatching(false)
    },
  })

  const handleStartMatch = () => {
    requestMatchMutation.mutate({
      userId,
      gender: formData.gender,
      ageGroup: formData.ageGroup,
      tags: formData.tags,
    })
  }

  const handleCancelMatch = () => {
    cancelMatchMutation.mutate()
  }

  const availableTags = [
    '文艺', '运动', '游戏', '音乐', '旅行', 
    '美食', '电影', '阅读', '科技', '艺术'
  ]

  const toggleTag = (tag: string) => {
    setFormData(prev => ({
      ...prev,
      tags: prev.tags.includes(tag)
        ? prev.tags.filter(t => t !== tag)
        : [...prev.tags, tag]
    }))
  }

  return (
    <div className="min-h-screen bg-background p-8">
      <div className="max-w-2xl mx-auto">
        <h1 className="text-4xl font-bold mb-8 text-center">绯悦匹配</h1>

        {!isMatching ? (
          <div className="space-y-6 bg-card p-8 rounded-lg border">
            <div>
              <label className="block text-sm font-medium mb-2">性别</label>
              <div className="flex gap-4">
                <button
                  onClick={() => setFormData(prev => ({ ...prev, gender: 'male' }))}
                  className={`flex-1 py-3 rounded-lg border ${
                    formData.gender === 'male'
                      ? 'bg-primary text-primary-foreground'
                      : 'bg-background'
                  }`}
                >
                  男
                </button>
                <button
                  onClick={() => setFormData(prev => ({ ...prev, gender: 'female' }))}
                  className={`flex-1 py-3 rounded-lg border ${
                    formData.gender === 'female'
                      ? 'bg-primary text-primary-foreground'
                      : 'bg-background'
                  }`}
                >
                  女
                </button>
              </div>
            </div>

            <div>
              <label className="block text-sm font-medium mb-2">年龄段</label>
              <select
                value={formData.ageGroup}
                onChange={(e) => setFormData(prev => ({ ...prev, ageGroup: e.target.value }))}
                className="w-full p-3 rounded-lg border bg-background"
              >
                <option value="<18">&lt;18</option>
                <option value="18-23">18-23</option>
                <option value="23+">23+</option>
              </select>
            </div>

            <div>
              <label className="block text-sm font-medium mb-2">
                兴趣标签 (最多选择5个)
              </label>
              <div className="flex flex-wrap gap-2">
                {availableTags.map(tag => (
                  <button
                    key={tag}
                    onClick={() => toggleTag(tag)}
                    disabled={formData.tags.length >= 5 && !formData.tags.includes(tag)}
                    className={`px-4 py-2 rounded-full border text-sm ${
                      formData.tags.includes(tag)
                        ? 'bg-primary text-primary-foreground'
                        : 'bg-background hover:bg-accent'
                    } disabled:opacity-50 disabled:cursor-not-allowed`}
                  >
                    {tag}
                  </button>
                ))}
              </div>
            </div>

            <button
              onClick={handleStartMatch}
              disabled={requestMatchMutation.isPending}
              className="w-full py-4 bg-primary text-primary-foreground rounded-lg font-semibold hover:opacity-90 disabled:opacity-50"
            >
              {requestMatchMutation.isPending ? '请求中...' : '开始匹配'}
            </button>

            {requestMatchMutation.isError && (
              <div className="text-destructive text-sm text-center">
                匹配请求失败，请重试
              </div>
            )}
          </div>
        ) : (
          <div className="bg-card p-8 rounded-lg border text-center space-y-6">
            <div className="text-6xl">🔍</div>
            <h2 className="text-2xl font-bold">正在匹配中...</h2>
            
            {matchStatus && (
              <div className="space-y-2">
                <p className="text-muted-foreground">
                  状态: {matchStatus.status}
                </p>
                <p className="text-muted-foreground">
                  队列位置: {matchStatus.position}
                </p>
              </div>
            )}

            <button
              onClick={handleCancelMatch}
              disabled={cancelMatchMutation.isPending}
              className="px-6 py-3 bg-destructive text-destructive-foreground rounded-lg hover:opacity-90"
            >
              取消匹配
            </button>
          </div>
        )}
      </div>
    </div>
  )
}
