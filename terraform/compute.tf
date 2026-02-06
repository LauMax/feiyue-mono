# CVM 实例（应用服务器）
resource "tencentcloud_instance" "app" {
  instance_name              = "${var.project_name}-${var.environment}-app"
  availability_zone          = var.availability_zone
  image_id                   = "img-eb30mz89"  # Ubuntu 22.04
  instance_type              = var.cvm_instance_type
  system_disk_type           = "CLOUD_PREMIUM"
  system_disk_size           = var.cvm_system_disk_size
  allocate_public_ip         = true
  internet_max_bandwidth_out = 10
  vpc_id                     = tencentcloud_vpc.main.id
  subnet_id                  = tencentcloud_subnet.public.id
  security_groups            = [tencentcloud_security_group.app.id]
  
  tags = merge(var.tags, {
    Name = "${var.project_name}-${var.environment}-app"
    Role = "application"
  })

  # 初始化脚本
  user_data = templatefile("${path.module}/scripts/app-init.sh", {
    environment = var.environment
  })
}

# PostgreSQL 实例
resource "tencentcloud_postgresql_instance" "main" {
  name              = "${var.project_name}-${var.environment}-postgres"
  availability_zone = var.availability_zone
  charge_type       = "POSTPAID_BY_HOUR"
  vpc_id            = tencentcloud_vpc.main.id
  subnet_id         = tencentcloud_subnet.private.id
  engine_version    = var.postgres_engine_version
  root_user         = "feiyue"
  root_password     = var.db_password
  charset           = "UTF8"
  
  spec_code = var.postgres_instance_type
  storage   = var.postgres_storage_size

  security_groups = [tencentcloud_security_group.db.id]
  
  tags = merge(var.tags, {
    Name = "${var.project_name}-${var.environment}-postgres"
  })
}

# Redis 实例
resource "tencentcloud_redis_instance" "main" {
  availability_zone  = var.availability_zone
  name               = "${var.project_name}-${var.environment}-redis"
  type_id            = 6  # Redis 5.0
  password           = var.redis_password
  mem_size           = 1024  # 1GB
  redis_shard_num    = 1
  redis_replicas_num = 1
  vpc_id             = tencentcloud_vpc.main.id
  subnet_id          = tencentcloud_subnet.private.id
  security_groups    = [tencentcloud_security_group.db.id]
  
  tags = merge(var.tags, {
    Name = "${var.project_name}-${var.environment}-redis"
  })
}

# 负载均衡器（可选，用于生产环境）
resource "tencentcloud_clb_instance" "main" {
  count = var.environment == "prod" ? 1 : 0
  
  network_type = "OPEN"
  clb_name     = "${var.project_name}-${var.environment}-lb"
  vpc_id       = tencentcloud_vpc.main.id
  subnet_id    = tencentcloud_subnet.public.id
  
  tags = var.tags
}
