import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from "./ui/card";
import { Button } from "./ui/button";
import type { Todo, User } from "@/types/todo";

interface TodoCardProps {
    todo: Todo;
    onDelete: (id: number) => void;
    onToggle: (id: number) => void;
}

export function TodoCard({ todo, onDelete, onToggle}: TodoCardProps) {
    const [isExpanded, setIsExpanded] = useState(false);
    const [user, setUser] = useState<User | null>(null);
    const [loadingUser, setLoadingUser] = useState(false);

    useEffect(() => {
        if (isExpanded && !user) {
            setLoadingUser(true);
            fetch(`http://localhost:5131/api/users/${todo.userId}`)
                .then(res => res.json())
                .then(data => setUser(data))
                .catch(err => console.error('Failed to fetch user:', err))
                .finally(() => setLoadingUser(false));
        }
    }, [isExpanded, todo.userId, user]);

    const cardBgColor = todo.isComplete ? 'bg-green-100 dark:bg-green-900' : 'bg-red-100 dark:border-red-900';
    const cardBorderColor = todo.isComplete ? 'bg-green-300 dark:bg-green-700' : 'bg-red-300 dark:border-red-700';
    
    return (
        <Card className={`${cardBgColor} ${cardBorderColor}`}>
            <CardHeader>
                <div className="flex justify-between items-start">
                    <div>
                        <CardTitle>{todo.title}</CardTitle>
                        <CardDescription>User ID: {todo.userId}</CardDescription>
                    </div>
                    <Button
                    variant="ghost"
                    size="sm"
                    onClick={()=> setIsExpanded(!isExpanded)}
                    >
                        {isExpanded ? '▼' : '▶'}
                    </Button>
                </div>
            </CardHeader>
            {isExpanded && (
                <>
                    <CardContent>
                        {loadingUser && <p>Loading user...</p>}
                        {user && (
                            <div className="mb-4 space-y-1">
                                <p><strong>Name:</strong> {user.name}</p>
                                <p><strong>Age:</strong> {user.age}</p>
                                <p><strong>Email:</strong> {user.email}</p>
                            </div>
                        )}
                        <p><strong>Description:</strong> {todo.description || 'No description provided.'}</p>
                    </CardContent>
                    <CardFooter className="flex justify-between">
                        <Button
                            variant={todo.isComplete ? 'secondary' : 'default'}
                            onClick={() => onToggle(todo.id)}
                        >
                            {todo.isComplete ? 'Mark as Incomplete' : 'Mark as Complete'}
                        </Button>
                        <div>            
                            <Link to={`/edit/${todo.id}`}>
                                <Button variant="outline" className="mr-2">Edit</Button>
                            </Link>

                            <Button variant="destructive" onClick={() => onDelete(todo.id)}>Delete</Button>
                        </div>
                    </CardFooter>
                </>
            )}
        </Card>
    );
}