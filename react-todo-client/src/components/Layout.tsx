import type { ReactNode } from "react";

interface LayoutProps {
    children: ReactNode;
}

export function Layout({ children}: LayoutProps) {
    return (
        <div className="container mx-auto px-4 py-8 max-w-4xl">
            {children}
        </div>
    );
}