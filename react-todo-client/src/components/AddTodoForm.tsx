import { useState } from "react";
import type { CreateTodoDto } from "../types/todo";
import { useTodos } from "@/hooks/useTodos";

interface AddTodoFormProps {
    onSubmit: (data: CreateTodoDto) => void;
}

export function AddTodoForm({ onSubmit }: AddTodoFormProps) {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        if (!title.trim()){
            return;
        }
        onSubmit({title, description})
        setTitle('');
        setDescription('');
    };


  return (
    <form onSubmit={handleSubmit}>
      <h2>Add New Todo</h2>
      <div>
        <label htmlFor="title">Title:</label>
        <input
          type="text"
          id="title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
      </div>
      <div>
        <label htmlFor="description">Description:</label>
        <input
          type="text"
          id="description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
      </div>
      <button type="submit">Add Todo</button>
    </form>
  );
}