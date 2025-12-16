import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { useTodos } from "@/hooks/useTodos";
import { Layout } from "@/components/Layout";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Checkbox } from "@/components/ui/checkbox";
import { Label } from "@/components/ui/label";
import { Card, CardContent, CardHeader, CardTitle, CardFooter } from "@/components/ui/card";
import type { UpdateTodoDto } from "@/types/todo";

interface TodoFormProps{
    mode: 'create' | 'edit';
}

export function TodoFormPage({ mode}: TodoFormProps) {
    const { id } = useParams<{ id: string}>();
    const navigate = useNavigate();
    const { addTodo, editTodo, todos } = useTodos();

    const [formData, setFormData] = useState<UpdateTodoDto>({
        title: '',
        description: '',
        isComplete: false,
        userId: 1, // default user
    });

    useEffect(() => {
        if (mode === 'edit' && id) {
            const todoToEdit = todos.find( t => t.id ===parseInt(id, 10));
            if(todoToEdit) {
                setFormData({
                    title: todoToEdit.title,
                    description: todoToEdit.description || '',
                    isComplete: todoToEdit.isComplete,
                    userId: todoToEdit.userId,
                });
            }
        }
    }, [id, mode, todos]);

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value} = e.target;
        setFormData(prev => ({ ...prev, [name]: name === 'userId' ? parseInt(value, 10) : value }));
    };

    const handleCheckboxChange = (checked: boolean) => {
        setFormData( prev => ({ ...prev, isComplete: checked}));
    };

    const handleSubmit = async ( event: React.FormEvent) => {
        event.preventDefault();
        if (!formData.title.trim()) return;

        if (mode==='create') {
            await addTodo({title: formData.title, description:formData.description, userId: formData.userId});
        } else if (mode ==='edit' && id) {
            await editTodo(parseInt(id), formData);
        }

        navigate('/');
    };

    return(
        <Layout>
            <Card className="max-w-2xl mx-auto">
                <CardHeader>
                    <CardTitle className="text-2xl">
                        {mode === 'edit' ? `Edit Todo #${id}` : 'Create New Todo'}
                    </CardTitle>
                </CardHeader>
                <form onSubmit={handleSubmit}>
                    <CardContent className="space-y-4">
                        <div className="space-y-2">
                        <Label htmlFor="title">Title</Label>
                        <Input
                            id="title"
                            name="title"
                            value={formData.title}
                            onChange={handleInputChange}
                            required
                        />
                        </div>
                         <div className="space-y-2">
                            <Label htmlFor="description">Description</Label>
                            <Input
                                id="description"
                                name="description"
                                value={formData.description}
                                onChange={handleInputChange}
                            />
                        </div>
                        <div className="space-y-2">
                            <Label htmlFor="userId">User ID</Label>
                            <Input
                                id="userId"
                                name="userId"
                                type="number"
                                value={formData.userId}
                                onChange={handleInputChange}
                                required
                            />
                        </div>
                        {mode === 'edit' && (
                        <div className="flex items-center space-x-2">
                            <Checkbox
                            id="isComplete"
                            checked={formData.isComplete}
                            onCheckedChange={handleCheckboxChange}
                            />
                            <Label htmlFor="isComplete">Completed</Label>
                        </div>
                        )}
                    </CardContent>
                     <CardFooter className="flex justify-end space-x-2 padding mt-3">
                        <Button type="button" variant="outline" onClick={() => navigate('/')}>
                        Cancel
                        </Button>
                        <Button type="submit">
                            {mode === 'edit' ? 'Update Todo' : 'Create Todo'}
                        </Button>
                     </CardFooter>
                </form>
            </Card>
        </Layout>
    );
}