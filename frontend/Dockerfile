FROM node:22-alpine AS build
WORKDIR /

COPY ./package.json .
COPY ./yarn.lock .
RUN yarn install --frozen-lockfile

COPY ./ .
RUN npm run build

FROM nginx:latest
COPY ./nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /dist/ /usr/share/nginx/html
EXPOSE 80
