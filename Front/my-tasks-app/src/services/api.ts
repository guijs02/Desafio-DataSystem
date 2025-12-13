import type { Task, CreateTaskInput, UpdateTaskInput, TasksResponse, GetAllTasksOutput } from '../types/task';

// Configure your API base URL here
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost:7086/api';

class ApiService {
  private baseUrl: string;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {}
  ): Promise<T> {
    const url = `${this.baseUrl}${endpoint}`;
    
    const config: RequestInit = {
      headers: {
        'Content-Type': 'application/json',
        ...options.headers,
      },
      ...options,
    };

    try {
      const response = await fetch(url, config);
      
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      // Handle empty responses (like DELETE)
      if (response.status === 204) {
        return {} as T;
      }

      const contentType = response.headers.get('content-type');
      if (contentType && contentType.includes('application/json')) {
        const text = await response.text();
        if (!text) {
          return {} as T;
        }
        return JSON.parse(text);
      }

      return {} as T;
    } catch (error) {
      console.error('API request failed:', error);
      throw error;
    }
  }

  async getAllTasks(pageNumber: number = 1, pageSize: number = 10): Promise<TasksResponse> {
    const response = await this.request<GetAllTasksOutput[] | TasksResponse>(
      `/task?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
    
    // Handle array response (GetAllTasksOutput[])
    if (Array.isArray(response)) {
      const firstItem = response[0];
      const tasks: Task[] = response.map(({ totalCount, pageNumber, pageSize, ...task }) => task);
      
      return {
        data: tasks,
        pageNumber: firstItem?.pageNumber ?? pageNumber,
        pageSize: firstItem?.pageSize ?? pageSize,
        totalCount: firstItem?.totalCount ?? response.length,
      };
    }
    
    return response;
  }

  async getTaskById(id: number): Promise<Task> {
    return this.request<Task>(`/task/${id}`);
  }

  async createTask(input: CreateTaskInput): Promise<Task> {
    return this.request<Task>('/task', {
      method: 'POST',
      body: JSON.stringify(input),
    });
  }

  async updateTask(input: UpdateTaskInput): Promise<Task> {
    return this.request<Task>(`/task`, {
      method: 'PUT',
      body: JSON.stringify(input),
    });
  }

  async deleteTask(id: number): Promise<void> {
    await this.request<void>(`/task/${id}`, {
      method: 'DELETE',
    });
  }
}

export const apiService = new ApiService(API_BASE_URL);

