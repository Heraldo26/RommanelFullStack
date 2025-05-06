FROM node:18-alpine as build
WORKDIR /app

COPY . .
RUN npm install
RUN npm run build --prod

FROM nginx:alpine
COPY --from=build /app/dist/rommanel-app/browser /usr/share/nginx/html

