export interface PagedOutput<T> {
  itens: T[];
  totalCount: number | null;
}
