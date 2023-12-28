FROM nginx:latest

# Use openssl to create an encryption key and a self-signed SSL certificate
#RUN openssl req -batch -x509 -nodes -days 365 -newkey rsa:2048 \ -keyout /etc/ssl/private/nginx.key \-out /etc/ssl/certs/nginx.crt


COPY nginx.conf "/etc/nginx/nginx.conf"
COPY api_json_errors.conf ./etc/nginx/api_json_errors.conf

COPY api_conf.d/ ./etc/nginx/api_conf.d/
# COPY api_conf.d/todo_api.conf ./etc/nginx/api_conf.d/todo_api.conf
COPY api_backends.conf ./etc/nginx/api_backends.conf
COPY api_gateway.conf ./etc/nginx/api_gateway.conf


COPY ssl/localhost.crt /etc/ssl/certs/localhost.crt
COPY ssl/localhost.key /etc/ssl/private/localhost.key

# docker build -t nginx-reverseproxy -f Nginx.Dockerfile .
# docker run -d -p 80:80 --name nginx-reverseproxy  nginx-reverseproxy
