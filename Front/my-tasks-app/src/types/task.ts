export enum Status {
  Pendente = 0,
  EmProgresso = 1,
  Concluida = 2,
}

export interface Task {
  id: number;
  title: string;
  description?: string;
  createdAt: string;
  finishAt?: string;
  status: Status;
}

export interface CreateTaskInput {
  title: string;
  description?: string;
  finishAt?: string;
  status?: Status;
}

export interface UpdateTaskInput {
  id: number;
  title?: string;
  description?: string;
  finishAt?: string;
  status?: Status;
}

export interface GetAllTasksOutput {
  id: number;
  title: string;
  description?: string;
  createdAt: string;
  finishAt?: string;
  status: Status;
  totalCount?: number;
  pageNumber?: number;
  pageSize?: number;
}

export interface TasksResponse {
  data: Task[];
  totalCount?: number;
  pageNumber?: number;
  pageSize?: number;
}
