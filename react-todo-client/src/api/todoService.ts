import type { Todo, CreateTodoDto, UpdateTodoDto } from '../types/todo';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

// GET all todos
export const getTodos = async (): Promise<Todo[]> => {
  const response = await fetch(`${API_BASE_URL}/todos`);
  if (!response.ok) {
    throw new Error('Failed to fetch todos');
  }
  return response.json();
};

// POST a new todo
export const createTodo = async (todoData: CreateTodoDto): Promise<Todo> => {
  const response = await fetch(`${API_BASE_URL}/todos`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(todoData),
  });

  if (!response.ok) {
    throw new Error('Failed to create todo');
  }
  return response.json();
};

//UPDATE a todo
export const updateTodo = async (id: number, todoData: UpdateTodoDto): Promise<void> => {
  const response = await fetch(`${API_BASE_URL}/todos/${id}`, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(todoData),
  });

  if (!response.ok) {
    throw new Error('Failed to update todo');
  }
};

// DELETE a todo
export const deleteTodo = async (id: number): Promise<void> => {
  const response = await fetch(`${API_BASE_URL}/todos/${id}`, {
    method: 'DELETE',
  });

  if (!response.ok) {
    throw new Error(`Failed to delete todo with id ${id}`);
  }
};

//Patch toggle
export const toggleTodoStatus = async (id: number): Promise<void> => {
  const response = await fetch(`${API_BASE_URL}/todos/${id}`, {
    method: 'PATCH',

  });

  if (!response.ok) {
    throw new Error(`Failed to toggle todo with id ${id}`);
  }
};