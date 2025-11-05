import type { Todo, UpdateTodoDto } from "@/types/todo";
import { TodoCard } from "@/components/TodoCard";

interface TodoListProps {
    todos: Todo[];
    onDelete: (id: number) => void;
    onToggle: (id: number) => void;
    onUpdate: (id: number, data: UpdateTodoDto) => void;
}

export function TodoList ({ todos, onDelete, onToggle, onUpdate }: TodoListProps) {
    if (todos.length === 0) {
        return (
            <div className="text-center py-10 px-4 border-2, border-dashed border-gray-300 rounded-lg">
                <h3 className="text-lg font-medium text-gray-900">No Todos Found</h3>
                <p className="mt-1 text-sm text-gray-500">
                    Create a new todo to get started!
                </p>
            </div>
        );
    }

    return (
        <div className="space-y-4">
            {todos.map(todo => (
                <TodoCard
                    key={todo.id}
                    todo={todo}
                    onDelete={onDelete}
                    onToggle={onToggle}
                    onUpdate={onUpdate}
                />
            ))}
        </div>
    );
}