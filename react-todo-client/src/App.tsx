import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { HomePage } from '@/pages/HomePage';
import { TodoFormPage } from "@/pages/TodoFormPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/create" element={<TodoFormPage mode="create" />} />
        <Route path="/edit/:id" element={<TodoFormPage mode="edit" />} />
      </Routes>
    </Router>
  );
}

export default App;

