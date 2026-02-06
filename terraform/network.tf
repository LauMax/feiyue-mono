# VPC 和子网
resource "tencentcloud_vpc" "main" {
  name       = "${var.project_name}-${var.environment}-vpc"
  cidr_block = "10.0.0.0/16"
  tags       = var.tags
}

resource "tencentcloud_subnet" "public" {
  name              = "${var.project_name}-${var.environment}-public-subnet"
  vpc_id            = tencentcloud_vpc.main.id
  availability_zone = var.availability_zone
  cidr_block        = "10.0.1.0/24"
  is_multicast      = false
  tags              = var.tags
}

resource "tencentcloud_subnet" "private" {
  name              = "${var.project_name}-${var.environment}-private-subnet"
  vpc_id            = tencentcloud_vpc.main.id
  availability_zone = var.availability_zone
  cidr_block        = "10.0.2.0/24"
  is_multicast      = false
  tags              = var.tags
}

# 安全组
resource "tencentcloud_security_group" "app" {
  name        = "${var.project_name}-${var.environment}-app-sg"
  description = "Security group for application servers"
  tags        = var.tags
}

# 允许 HTTP/HTTPS
resource "tencentcloud_security_group_rule" "app_http" {
  security_group_id = tencentcloud_security_group.app.id
  type              = "ingress"
  cidr_ip           = "0.0.0.0/0"
  ip_protocol       = "tcp"
  port_range        = "80,443"
  policy            = "accept"
}

# 允许 SSH（仅限指定 IP）
resource "tencentcloud_security_group_rule" "app_ssh" {
  security_group_id = tencentcloud_security_group.app.id
  type              = "ingress"
  cidr_ip           = "0.0.0.0/0"  # 生产环境应限制为特定 IP
  ip_protocol       = "tcp"
  port_range        = "22"
  policy            = "accept"
}

# 允许所有出站流量
resource "tencentcloud_security_group_rule" "app_egress" {
  security_group_id = tencentcloud_security_group.app.id
  type              = "egress"
  cidr_ip           = "0.0.0.0/0"
  ip_protocol       = "all"
  policy            = "accept"
}

# 数据库安全组
resource "tencentcloud_security_group" "db" {
  name        = "${var.project_name}-${var.environment}-db-sg"
  description = "Security group for database servers"
  tags        = var.tags
}

# 允许应用服务器访问数据库
resource "tencentcloud_security_group_rule" "db_from_app" {
  security_group_id = tencentcloud_security_group.db.id
  type              = "ingress"
  cidr_ip           = "10.0.0.0/16"  # VPC 内部
  ip_protocol       = "tcp"
  port_range        = "5432,6379"   # PostgreSQL + Redis
  policy            = "accept"
}
