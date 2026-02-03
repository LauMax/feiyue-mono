import { clsx, type ClassValue } from 'clsx'
import { twMerge } from 'tailwind-merge'

/**
 * Tailwind CSS 类名合并工具 - 参考 Studio 的 utils
 */
export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}
