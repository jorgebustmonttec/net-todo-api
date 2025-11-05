import { useState } from "react";
import type { Todo } from "@/types/todo";
import { Link } from "react-router-dom";

//import shadcn stuff
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card';

//define props, needs todo and functions for buttons
interface TodoCardProps {
    todo: Todo;
    onDelete: (id: number) => void;
    onToggle: (id: number) => void;
}

export function TodoCard({ todo, onDelete, onToggle}: TodoCardProps) {
    const [isExpanded, setIsExpanded] = useState(false);

    //card color based on wether its complete or not
    const cardBgColor = todo.isComplete ? 'bg-green-100 dark:bg-green-900' : 'bg-red-100 dark:border-red-900';
    const cardBorderColor = todo.isComplete ? 'bg-green-300 dark:bg-green-700' : 'bg-red-300 dark:border-red-700';
    
    return (
        <Card className={`${cardBgColor} ${cardBorderColor}`}>
            <CardHeader>
                <div className="flex justify-between items-start">
                    <div>
                        <CardTitle>{todo.title}</CardTitle>
                        <CardDescription>User ID: {todo.user}</CardDescription>
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
                        <p>{todo.description || 'No description provided.'}</p>
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