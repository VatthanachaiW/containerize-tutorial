FROM node:20-alpine AS base
RUN npm isntall -g pnpm
RUN corepack enable
COPY . /app
WORKDIR /app
RUN pnpm install --frozen-lockfile && \
    pnpm run build
EXPOSE 3000
CMD [ "node",".output/server/index.mjs" ]