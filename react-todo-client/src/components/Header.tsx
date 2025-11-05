import { Link } from "react-router-dom";
import { Button } from "@/components/ui/button";

export function Header() {
    return (
        <header className="flex justify-between items-center mb-6 pb-6">
            <h1 className="text-3xl font-bold">Your Todos</h1>

            <Link to={"/create"}>
                <Button>Create New Todo</Button>
            </Link>
        </header>
    );
}