# Docker

1. Start the server

```bash
 docker-compose -f .\docker-compose.yml up -d
```

2. Down the server

```bash
docker-compose -f .\docker-compose.yml down
```

## Start in Dev

```bash
npm run dev
```

### STEPS for creating Frontend APP VITE/REACT/TAILWIND

## Create your project

```bash
npm create vite@latest frontend-weather-app -- --template react
cd my-project
```

## Install Tailwind CSS

```bash
npm install -D tailwindcss postcss autoprefixer
npx tailwindcss init -p
```

## Configure your template paths

`tailwind.config.js`

```js
/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
```

## Add the Tailwind directives to your CSS

`./src/index.css`

```css
@tailwind base;
@tailwind components;
@tailwind utilities;
```

---

### /vite.config.js

```javascript
import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: true,
    strictPort: true,
    port: 8080,
  },
});
```