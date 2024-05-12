import { defineConfig, loadEnv } from 'vite';
import react from '@vitejs/plugin-react';
import tailwind from 'tailwindcss';
import { resolve } from 'path';

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '');
  return {
    define: {
      'process.env.BACKEND_URL': JSON.stringify(env.BACKEND_URL)
    },
    plugins: [react()],
    css: {
      postcss: {
        plugins: [tailwind()]
      }
    },
    preview: {
      host: true,
      port: 7777
    },
    resolve: {
      alias: {
        $assets: resolve('./src/assets')
      }
    }
  };
});
