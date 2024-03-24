import { Suspense } from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { HelmetProvider } from 'react-helmet-async';
import { CookiesProvider } from "react-cookie";
import axios from "axios";
import AuthProvider from "src/hooks/auth-provider";

import App from './app';

axios.defaults.withCredentials = true;

// ----------------------------------------------------------------------

const root = ReactDOM.createRoot(document.getElementById('root')!);

root.render(
    <CookiesProvider>
        <HelmetProvider>
            <AuthProvider>
                <BrowserRouter>
                    <Suspense>
                        <App />
                    </Suspense>
                </BrowserRouter>
            </AuthProvider>
        </HelmetProvider>
    </CookiesProvider>
);