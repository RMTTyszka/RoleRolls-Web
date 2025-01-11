export interface PagedOutput<T> {
  items: T[];
  totalCount: number | null;
}
