# Use the official Node.js 18 image as a base
FROM node:18-alpine

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm i

# Copy local code to the container's workspace.
COPY . .

# Build the application
RUN npm run build

# Set the command to start the application
CMD [ "npm", "run", "dev" ]