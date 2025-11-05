import { useState, useEffect } from 'react';
import type { Todo, CreateTodoDto, UpdateTodoDto} from '@/types/todo';
import { getTodos, createTodo, deleteTodo,updateTodo,toggleTodoStatus } from '@/api/todoService';

export const useTodos = () => {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchInitialTodos = async () => {
      try {
        setIsLoading(true);
        const data = await getTodos();
        setTodos(data);
      } catch (err) {
        setError('Failed to load todos.');
        console.error(err);
      } finally {
        setIsLoading(false);
      }
    };
    fetchInitialTodos();
  }, []);

  const addTodo = async (todoData: CreateTodoDto) => {
    try {
      const newTodoFromServer = await createTodo(todoData);
      setTodos(currentTodos => [...currentTodos, newTodoFromServer]);
    } catch (err) {
      setError('Failed to add todo.');
      console.error(err);
    }
  };

  const removeTodo = async (id: number) => {
    try {
      await deleteTodo(id);
      setTodos(currentTodos => currentTodos.filter(todo => todo.id !== id));
    } catch (err) {
      setError(`Failed to remove todo with id ${id}.`);
      console.error(err);
    }
  };

  const toggleTodo = async (id: number) => {
    try {
        await toggleTodoStatus(id);

        setTodos(currentTodos => currentTodos.map(todo => todo.id === id ? {...todo, isComplete: !todo.isComplete} : todo));
    } catch (err) {
        setError(`Failed to toggle tod with id: ${id}.`);
        console.error(err);
    }
  }

  const editTodo = async (id: number, todoData: UpdateTodoDto) => {
    try {
        await updateTodo(id, todoData);

        setTodos(currentTodos => currentTodos.map(todo => todo.id === id ?{...todo, ...todoData} : todo ))
    } catch (error) {
        
    }
  }

  return { todos, isLoading, error, addTodo, removeTodo, toggleTodo, editTodo };
};