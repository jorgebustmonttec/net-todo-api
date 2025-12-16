import { useState } from "react";
import type { Todo, UpdateTodoDto } from "@/types/todo";

interface TodoItemProps {
    todo: Todo;
    onDelete: (id: number) => void;
    onToggle: (id: number) => void;
    onUpdate: (id: number, updateDto: UpdateTodoDto) => void;
}

export function TodoItem ({todo, onDelete, onToggle, onUpdate}: TodoItemProps){
    const [isEditing, setIsEditing] = useState(false);

    const [editData, setEditData] = useState<UpdateTodoDto>({
        title: todo.title,
        description: todo.description || '',
        isComplete: todo.isComplete,
        userId: todo.userId,
    });

    const handleEdit = () => {
        setEditData({
            title: todo.title,
            description: todo.description || '',
            isComplete: todo.isComplete,
            userId: todo.userId,
        });
        setIsEditing(true);
    };

    const handleSave = () => {
        onUpdate(todo.id, editData);
        setIsEditing(false);
    };

    const handleCancel = () =>{
        setEditData({
            title: todo.title,
            description: todo.description || '',
            isComplete: todo.isComplete,
            userId: todo.userId,
        });
        setIsEditing(false);
    };

    if (isEditing){
        return(
            <tr>
                <td>{todo.id}</td>
                <td>
                    <input 
                    type="text" 
                    value={editData.title}
                    onChange={(e) => setEditData({ ...editData, title: e.target.value })}
                    />
                </td>
                <td>
                    <input 
                    type="text" 
                    value={editData.description}
                    onChange={(e) => setEditData({ ...editData, description: e.target.value })}
                    />
                </td>
                <td>{todo.userId}</td>
                <td>
                    <input 
                    type="checkbox"
                    checked={editData.isComplete}
                    onChange={(e) => setEditData({ ...editData, isComplete: e.target.checked })}
                    />
                </td>
                <td>
                    <button onClick={handleSave}>Save</button>
                    <button onClick={handleCancel}>Cancel</button>
                </td>
            </tr>
        );
    }
    return (
        <tr>
            <td>{todo.id}</td>
            <td>{todo.title}</td>
            <td>{todo.description}</td>
            <td>{todo.userId}</td>
            <td>
                <input 
                type="checkbox" 
                checked={todo.isComplete} 
                onChange={() => onToggle(todo.id)}
                />
            </td>
            <td>
                <button onClick={handleEdit}>Edit</button>
                <button onClick={() => onDelete(todo.id)}>Delete</button>
            </td>
        </tr>
    );
}