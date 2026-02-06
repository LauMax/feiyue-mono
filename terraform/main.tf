# Feiyue 腾讯云基础设施配置

terraform {
  required_version = ">= 1.0"
  
  required_providers {
    tencentcloud = {
      source  = "tencentcloudstack/tencentcloud"
      version = "~> 1.81"
    }
  }

  # 后端配置 - 使用腾讯云 COS 存储 Terraform 状态
  backend "cos" {
    region = "ap-guangzhou"
    bucket = "feiyue-terraform-state-1234567890"  # 需要提前创建
    prefix = "terraform/state"
  }
}

# 腾讯云提供商配置
provider "tencentcloud" {
  region = var.region
}
