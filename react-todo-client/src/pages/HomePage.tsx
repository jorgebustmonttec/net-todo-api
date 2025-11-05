import { useTodos } from "@/hooks/useTodos";
import { Layout } from "@/components/Layout";
import { TodoList } from "@/components/TodoList";
import { Header } from "@/components/Header";

export function HomePage (){
  const { todos, isLoading, error, removeTodo, toggleTodo, editTodo } = useTodos();

  if (isLoading) {
    return (
      <Layout>
        <Header/ >
          <div className="text-center py-10">Loading...</div>
      </Layout>
    );
  }

  return (
    <Layout>
      <Header/>
      <div className="mt8">
        <TodoList
        todos={todos}
        onDelete={removeTodo}
        onToggle={toggleTodo}
        onUpdate={editTodo}
        />
      </div>
    </Layout>
  );
}