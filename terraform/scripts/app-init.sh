#!/bin/bash
# Feiyue 应用服务器初始化脚本

set -e

echo "=== Starting Feiyue App Server Initialization ==="

# 更新系统
apt-get update
apt-get upgrade -y

# 安装必要工具
apt-get install -y \
    curl \
    wget \
    git \
    unzip \
    software-properties-common \
    apt-transport-https

# 安装 .NET 10 Runtime
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 10.0 --runtime aspnetcore
./dotnet-install.sh --channel 10.0 --runtime dotnet

# 添加 .NET 到 PATH
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> /etc/profile.d/dotnet.sh
echo 'export PATH=$PATH:$DOTNET_ROOT' >> /etc/profile.d/dotnet.sh

# 安装 Docker
curl -fsSL https://get.docker.com -o get-docker.sh
sh get-docker.sh
systemctl enable docker
systemctl start docker

# 安装 Docker Compose
curl -L "https://github.com/docker/compose/releases/download/v2.24.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose

# 创建应用目录
mkdir -p /opt/feiyue
chown -R ubuntu:ubuntu /opt/feiyue

# 配置防火墙（允许 HTTP/HTTPS）
ufw allow 80/tcp
ufw allow 443/tcp
ufw allow 22/tcp
ufw --force enable

# 创建 systemd 服务文件
cat > /etc/systemd/system/feiyue.service <<EOF
[Unit]
Description=Feiyue Backend Service
After=network.target

[Service]
Type=notify
User=ubuntu
WorkingDirectory=/opt/feiyue
ExecStart=/root/.dotnet/dotnet /opt/feiyue/Feiyue.Api.dll
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=feiyue
Environment=ASPNETCORE_ENVIRONMENT=${environment}
Environment=DOTNET_ROOT=/root/.dotnet

[Install]
WantedBy=multi-user.target
EOF

# 重新加载 systemd
systemctl daemon-reload

echo "=== Feiyue App Server Initialization Complete ==="
