# CVM 公网 IP
output "app_public_ip" {
  description = "Application server public IP"
  value       = tencentcloud_instance.app.public_ip
}

# CVM 内网 IP
output "app_private_ip" {
  description = "Application server private IP"
  value       = tencentcloud_instance.app.private_ip
}

# PostgreSQL 连接信息
output "postgres_endpoint" {
  description = "PostgreSQL connection endpoint"
  value       = tencentcloud_postgresql_instance.main.private_access_ip
  sensitive   = true
}

output "postgres_port" {
  description = "PostgreSQL port"
  value       = tencentcloud_postgresql_instance.main.private_access_port
}

# Redis 连接信息
output "redis_endpoint" {
  description = "Redis connection endpoint"
  value       = tencentcloud_redis_instance.main.ip
  sensitive   = true
}

output "redis_port" {
  description = "Redis port"
  value       = tencentcloud_redis_instance.main.port
}

# 连接字符串（用于应用配置）
output "connection_strings" {
  description = "Database connection strings"
  value = {
    postgres = "Host=${tencentcloud_postgresql_instance.main.private_access_ip};Port=${tencentcloud_postgresql_instance.main.private_access_port};Database=feiyue;Username=feiyue;Password=${var.db_password}"
    redis    = "${tencentcloud_redis_instance.main.ip}:${tencentcloud_redis_instance.main.port}"
  }
  sensitive = true
}
