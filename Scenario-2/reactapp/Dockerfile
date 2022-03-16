FROM node:latest as build

WORKDIR /app

COPY ./package.json /app/package.json
COPY ./package-lock.json /app/package-lock.json

RUN npm ci
COPY . .
RUN npm run build


FROM nginx:latest
COPY --from=build /app/dist /usr/share/nginx/html/
COPY ./nginx/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80:80


# Steps:
# docker network create --subnet=172.33.0.0/16 rabbitmq_go_net
# docker network rm  rabbitmq_go_net
# docker build -t reactapp .
# docker run --net rabbitmq_go_net -p 80:80 --name reactapp  --ip 172.33.0.100 -d reactapp 

