import { useState, useEffect } from 'react';
import { apiService } from './services/api';
import type { Task, CreateTaskInput, UpdateTaskInput } from './types/task';
import { Status } from './types/task';
import './App.css';

// Configuração de status
const STATUS_CONFIG = {
  [Status.Pendente]: { label: 'Pendente', class: 'pending' },
  [Status.EmProgresso]: { label: 'Em Progresso', class: 'in-progress' },
  [Status.Concluida]: { label: 'Concluída', class: 'completed' },
} as const;

// Estado inicial do formulário
const INITIAL_FORM_DATA: CreateTaskInput = {
  title: '',
  description: '',
  finishAt: '',
  status: Status.Pendente,
};

function App() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(10);
  const [totalCount, setTotalCount] = useState(0);
  const [statusFilter, setStatusFilter] = useState<Status | null>(null);
  const [allTasks, setAllTasks] = useState<Task[]>([]);

  // Form states
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [editingTask, setEditingTask] = useState<Task | null>(null);
  const [formData, setFormData] = useState<CreateTaskInput>(INITIAL_FORM_DATA);

  // Load tasks
  const loadTasks = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await apiService.getAllTasks(pageNumber, pageSize);
      const loadedTasks = response.data || response;
      setAllTasks(loadedTasks);
      setTotalCount(response.totalCount || response.data?.length || 0);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao carregar tarefas');
      console.error('Error loading tasks:', err);
    } finally {
      setLoading(false);
    }
  };

  // Filter tasks by status
  useEffect(() => {
    setTasks(statusFilter === null ? allTasks : allTasks.filter(task => task.status === statusFilter));
  }, [statusFilter, allTasks]);

  useEffect(() => {
    loadTasks();
  }, [pageNumber]);

  // Validações
  const validateForm = (): string | null => {
    if (formData.description && formData.description.length > 100) {
      return 'A descrição não pode ter mais de 100 caracteres.';
    }
    
    if (formData.finishAt) {
      const finishDate = new Date(formData.finishAt);
      const comparisonDate = editingTask?.createdAt ? new Date(editingTask.createdAt) : new Date();
      
      if (finishDate < comparisonDate) {
        return editingTask 
          ? 'A data de finalização não pode ser anterior à data de criação da tarefa.'
          : 'A data de finalização não pode ser anterior à data de criação.';
      }
    }
    
    return null;
  };

  // Reset form
  const resetForm = () => {
    setShowCreateForm(false);
    setEditingTask(null);
    setFormData(INITIAL_FORM_DATA);
  };

  // Open create form
  const openCreateForm = () => {
    setEditingTask(null);
    setFormData(INITIAL_FORM_DATA);
    setShowCreateForm(true);
  };

  // Create task
  const handleCreate = async (e: React.FormEvent) => {
    e.preventDefault();
    const validationError = validateForm();
    if (validationError) {
      setError(validationError);
      return;
    }

    setLoading(true);
    setError(null);
    try {
      const submitData: CreateTaskInput = {
        ...formData,
        finishAt: formData.finishAt ? new Date(formData.finishAt).toISOString() : undefined,
      };
      await apiService.createTask(submitData);
      resetForm();
      await loadTasks();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao criar tarefa');
    } finally {
      setLoading(false);
    }
  };

  // Update task
  const handleUpdate = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!editingTask) return;

    const validationError = validateForm();
    if (validationError) {
      setError(validationError);
      return;
    }

    setLoading(true);
    setError(null);
    try {
      const updateInput: UpdateTaskInput = {
        id: editingTask.id,
        title: formData.title || editingTask.title,
        description: formData.description,
        finishAt: formData.finishAt ? new Date(formData.finishAt).toISOString() : undefined,
        status: formData.status,
      };
      await apiService.updateTask(updateInput);
      resetForm();
      await loadTasks();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao atualizar tarefa');
    } finally {
      setLoading(false);
    }
  };

  // Delete task
  const handleDelete = async (id: number) => {
    if (!confirm('Tem certeza que deseja excluir esta tarefa?')) return;

    setLoading(true);
    setError(null);
    try {
      await apiService.deleteTask(id);
      await loadTasks();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Erro ao excluir tarefa');
    } finally {
      setLoading(false);
    }
  };

  // Start editing
  const startEdit = (task: Task) => {
    setEditingTask(task);
    setFormData({
      title: task.title,
      description: task.description || '',
      finishAt: task.finishAt ? new Date(task.finishAt).toISOString().slice(0, 16) : '',
      status: task.status,
    });
    setShowCreateForm(false);
  };

  // Format date for display
  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('pt-BR', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  };

  // Clear error when typing
  const handleInputChange = (field: keyof CreateTaskInput, value: string | Status) => {
    setFormData({ ...formData, [field]: value });
    if (error) setError(null);
  };

  const totalPages = Math.ceil(totalCount / pageSize);
  const filteredCount = statusFilter === null ? totalCount : tasks.length;

  return (
    <div className="app-container">
      <header>
        <h1>Gerenciamento de Tarefas</h1>
      </header>

      {error && (
        <div className="error-message">
          <p>{error}</p>
          <button onClick={() => setError(null)} aria-label="Fechar erro">
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
              <line x1="18" y1="6" x2="6" y2="18"></line>
              <line x1="6" y1="6" x2="18" y2="18"></line>
            </svg>
          </button>
        </div>
      )}

      <div className="actions-bar">
        <div className="filter-container">
          <span className="filter-label">Filtrar por status:</span>
          <div className="filter-buttons">
            {[null, Status.Pendente, Status.EmProgresso, Status.Concluida].map((status) => (
              <button
                key={status ?? 'all'}
                className={`filter-btn ${statusFilter === status ? 'active' : ''}`}
                onClick={() => setStatusFilter(status)}
                disabled={loading}
              >
                {status === null ? 'Todos' : STATUS_CONFIG[status].label}
              </button>
            ))}
          </div>
        </div>
        <button
          className="btn btn-primary"
          onClick={openCreateForm}
          disabled={loading}
        >
          <svg className="btn-icon-svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
            <line x1="12" y1="5" x2="12" y2="19"></line>
            <line x1="5" y1="12" x2="19" y2="12"></line>
          </svg>
          Nova Tarefa
        </button>
      </div>

      {(showCreateForm || editingTask) && (
        <div className="form-container">
          <h2>{editingTask ? 'Editar Tarefa' : 'Nova Tarefa'}</h2>
          <form onSubmit={editingTask ? handleUpdate : handleCreate}>
            <div className="form-group">
              <label htmlFor="title">Título *</label>
              <input
                type="text"
                id="title"
                value={formData.title}
                onChange={(e) => handleInputChange('title', e.target.value)}
                required
                disabled={loading}
              />
            </div>

            <div className="form-group">
              <label htmlFor="description">
                Descrição
                <span className="char-counter">
                  ({formData.description?.length || 0}/100)
                </span>
              </label>
              <textarea
                id="description"
                value={formData.description}
                onChange={(e) => {
                  const value = e.target.value;
                  if (value.length <= 100) {
                    handleInputChange('description', value);
                  }
                }}
                rows={4}
                disabled={loading}
                maxLength={100}
              />
              {formData.description && formData.description.length > 90 && (
                <small className="char-warning">
                  Restam {100 - (formData.description.length || 0)} caracteres
                </small>
              )}
            </div>

            <div className="form-group">
              <label htmlFor="finishAt">Finalizar em</label>
              <input
                type="datetime-local"
                id="finishAt"
                value={formData.finishAt}
                onChange={(e) => handleInputChange('finishAt', e.target.value)}
                onBlur={(e) => {
                  if (e.target.value) {
                    const validationError = validateForm();
                    if (validationError) setError(validationError);
                  }
                }}
                disabled={loading}
              />
            </div>

            <div className="form-group">
              <label htmlFor="status">Status *</label>
              <select
                id="status"
                value={formData.status}
                onChange={(e) => handleInputChange('status', Number(e.target.value) as Status)}
                required
                disabled={loading}
              >
                {Object.entries(STATUS_CONFIG).map(([value, config]) => (
                  <option key={value} value={value}>
                    {config.label}
                  </option>
                ))}
              </select>
            </div>

            <div className="form-actions">
              <button
                type="submit"
                className="btn btn-primary"
                disabled={loading}
              >
                {loading ? 'Salvando...' : editingTask ? 'Atualizar' : 'Criar'}
              </button>
              <button
                type="button"
                className="btn btn-secondary"
                onClick={resetForm}
                disabled={loading}
              >
                Cancelar
              </button>
            </div>
          </form>
        </div>
      )}

      <div className="tasks-container">
        {loading && !tasks.length ? (
          <div className="loading">Carregando tarefas...</div>
        ) : tasks.length === 0 ? (
          <div className="empty-state">
            <p>
              {statusFilter !== null
                ? `Nenhuma tarefa encontrada com status "${STATUS_CONFIG[statusFilter].label}".`
                : 'Nenhuma tarefa encontrada.'}
            </p>
            {statusFilter !== null ? (
              <p>
                <button
                  className="btn btn-secondary"
                  onClick={() => setStatusFilter(null)}
                  style={{ marginTop: '1rem' }}
                >
                  Ver todas as tarefas
                </button>
              </p>
            ) : (
              <p>Crie sua primeira tarefa clicando em "Nova Tarefa".</p>
            )}
          </div>
        ) : (
          <>
            <div className="tasks-list">
              {tasks.map((task) => {
                const statusInfo = STATUS_CONFIG[task.status];
                return (
                  <div
                    key={task.id}
                    className={`task-card ${statusInfo.class}`}
                  >
                    <div className="task-header">
                      <h3>{task.title}</h3>
                      <div className="task-actions">
                        <button
                          className="btn-icon"
                          onClick={() => startEdit(task)}
                          disabled={loading}
                          title="Editar"
                          aria-label="Editar tarefa"
                        >
                          <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                            <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7"></path>
                            <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z"></path>
                          </svg>
                        </button>
                        <button
                          className="btn-icon"
                          onClick={() => handleDelete(task.id)}
                          disabled={loading}
                          title="Excluir"
                          aria-label="Excluir tarefa"
                        >
                          <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                            <polyline points="3 6 5 6 21 6"></polyline>
                            <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"></path>
                          </svg>
                        </button>
                      </div>
                    </div>
                    {task.description && (
                      <p className="task-description">{task.description}</p>
                    )}
                    <div className="task-footer">
                      <span className={`status-badge ${statusInfo.class}`}>
                        {task.status === Status.Concluida && (
                          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="3">
                            <polyline points="20 6 9 17 4 12"></polyline>
                          </svg>
                        )}
                        {task.status === Status.EmProgresso && (
                          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                            <circle cx="12" cy="12" r="10"></circle>
                            <polyline points="12 6 12 12 16 14"></polyline>
                          </svg>
                        )}
                        {task.status === Status.Pendente && (
                          <svg width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2">
                            <circle cx="12" cy="12" r="10"></circle>
                          </svg>
                        )}
                        {statusInfo.label}
                      </span>
                      <div className="task-dates">
                        {task.createdAt && (
                          <span className="task-date">
                            Criada: {formatDate(task.createdAt)}
                          </span>
                        )}
                        {task.finishAt && (
                          <span className="task-date finish-date">
                            Finalizar: {formatDate(task.finishAt)}
                          </span>
                        )}
                      </div>
                    </div>
                  </div>
                );
              })}
            </div>

            {totalPages > 1 && (
              <div className="pagination">
                <button
                  className="btn btn-secondary"
                  onClick={() => setPageNumber((p) => Math.max(1, p - 1))}
                  disabled={pageNumber === 1 || loading}
                >
                  ← Anterior
                </button>
                <span className="page-info">
                  Página {pageNumber} de {totalPages} ({filteredCount} tarefas{statusFilter !== null ? ' filtradas' : ''})
                </span>
                <button
                  className="btn btn-secondary"
                  onClick={() => setPageNumber((p) => Math.min(totalPages, p + 1))}
                  disabled={pageNumber === totalPages || loading}
                >
                  Próxima →
                </button>
              </div>
            )}
          </>
        )}
      </div>
    </div>
  );
}

export default App;
