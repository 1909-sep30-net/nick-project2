# newest lts
FROM node:12 as build

WORKDIR /app

# restore dependencies as separate layer
COPY package.json package-lock.json ./
RUN npm ci

COPY . ./

# build-time configuration
ARG fridge_api_base_url=https://1909nickproject2api.azurewebsites.net
RUN sed -i "s|https://1909nickproject2api.azurewebsites.net|$fridge_api_base_url|" src/environments/environment.prod.ts

RUN npx ng build --prod

# stable
FROM nginx:1.16

WORKDIR /usr/share/nginx/html

# remove default nginx page
RUN rm -rf *
# config server for deep linking
COPY nginx-custom.conf /etc/nginx/conf.d/default.conf

COPY --from=build /app/dist/* ./
