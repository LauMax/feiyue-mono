# 环境配置
variable "environment" {
  description = "Environment name (dev, staging, prod)"
  type        = string
  default     = "dev"
}

# 地域配置
variable "region" {
  description = "Tencent Cloud region"
  type        = string
  default     = "ap-guangzhou"  # 广州
}

# 可用区配置
variable "availability_zone" {
  description = "Availability zone"
  type        = string
  default     = "ap-guangzhou-3"
}

# 项目名称
variable "project_name" {
  description = "Project name"
  type        = string
  default     = "feiyue"
}

# CVM 实例配置
variable "cvm_instance_type" {
  description = "CVM instance type"
  type        = string
  default     = "S5.MEDIUM4"  # 2核4G
}

# CVM 系统盘大小
variable "cvm_system_disk_size" {
  description = "System disk size in GB"
  type        = number
  default     = 50
}

# PostgreSQL 实例配置
variable "postgres_instance_type" {
  description = "PostgreSQL instance type"
  type        = string
  default     = "cdb.pg.s2.medium"  # 2核4G
}

variable "postgres_engine_version" {
  description = "PostgreSQL engine version"
  type        = string
  default     = "14.2"
}

variable "postgres_storage_size" {
  description = "PostgreSQL storage size in GB"
  type        = number
  default     = 50
}

# Redis 实例配置
variable "redis_instance_type" {
  description = "Redis instance type"
  type        = string
  default     = "redis.master.small.default"  # 1G内存
}

variable "redis_version" {
  description = "Redis version"
  type        = string
  default     = "5.0"
}

# 数据库密码（应该从环境变量或 Vault 获取）
variable "db_password" {
  description = "Database password"
  type        = string
  sensitive   = true
}

# Redis 密码
variable "redis_password" {
  description = "Redis password"
  type        = string
  sensitive   = true
}

# 标签
variable "tags" {
  description = "Resource tags"
  type        = map(string)
  default = {
    Project     = "Feiyue"
    ManagedBy   = "Terraform"
  }
}
