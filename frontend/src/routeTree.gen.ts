import { createRootRoute, createRoute, createRouter, Outlet } from '@tanstack/react-router'

// Root 布局
const rootRoute = createRootRoute({
  component: () => (
    <div className="min-h-screen bg-background">
      <Outlet />
    </div>
  ),
})

// 首页
const indexRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/',
  component: () => (
    <div className="flex items-center justify-center min-h-screen">
      <div className="text-center">
        <h1 className="text-4xl font-bold mb-4">绯悦</h1>
        <p className="text-muted-foreground mb-8">匿名角色扮演聊天</p>
        <div className="space-x-4">
          <a href="/match" className="inline-block px-6 py-3 bg-primary text-primary-foreground rounded-lg">
            开始匹配
          </a>
        </div>
      </div>
    </div>
  ),
})

// 匹配页面
const matchRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/match',
  component: () => {
    const { MatchPage } = await import('./pages/MatchPage')
    return <MatchPage />
  },
})

// 聊天页面
const chatRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/chat/$roomId',
  component: () => (
    <div className="container mx-auto p-8">
      <h1 className="text-3xl font-bold mb-6">聊天室</h1>
      <p>TODO: 实现聊天界面</p>
    </div>
  ),
})

// 创建路由树
export const routeTree = rootRoute.addChildren([
  indexRoute,
  matchRoute,
  chatRoute,
])

export type RouteTree = typeof routeTree
