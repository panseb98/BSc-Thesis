export interface ResponseModel<T> {
    data: T;
    succeeded: boolean;
    errors: Array<string>
    message: string;
  }