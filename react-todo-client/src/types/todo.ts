export interface Todo{
    id: number;
    title: string;
    isComplete: boolean;
    description: string | null;
    user: number;
}

export type CreateTodoDto = {
  title: string;
  description?: string; 
};

export type UpdateTodoDto = {
    title: string;
    description?: string;
    isComplete: boolean;
}