export interface Todo{
    id: number;
    title: string;
    isComplete: boolean;
    description: string | null;
    userId: number;
}

export interface User {
    id: number;
    name: string;
    age: number;
    email: string;
}

export type CreateTodoDto = {
  title: string;
  description?: string;
  userId: number;
};

export type UpdateTodoDto = {
    title: string;
    description?: string;
    isComplete: boolean;
    userId: number;
}