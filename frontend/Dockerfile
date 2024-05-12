FROM node:22-alpine
WORKDIR /

COPY ./package.json .
COPY ./yarn.lock .
RUN yarn install --frozen-lockfile

COPY ./.env.production ./.env
COPY ./ .
RUN npm run build
EXPOSE 7777
ENTRYPOINT [ "npm", "run", "preview" ]
