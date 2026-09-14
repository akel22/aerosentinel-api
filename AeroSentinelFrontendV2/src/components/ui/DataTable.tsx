import { ArrowDown, ArrowUp, ArrowUpDown, ChevronLeft, ChevronRight, Search } from "lucide-react";
import { useMemo, useState } from "react";
import type { DataTableColumn } from "../../types";
import { cx } from "../../utils/format";

type SortDirection = "asc" | "desc";

interface DataTableProps<T extends { id: string; searchText: string }> {
  title: string;
  rows: T[];
  columns: Array<DataTableColumn<T>>;
  searchPlaceholder: string;
  initialPageSize?: number;
}

export function DataTable<T extends { id: string; searchText: string }>({
  title,
  rows,
  columns,
  searchPlaceholder,
  initialPageSize = 6,
}: DataTableProps<T>) {
  const firstSortable = columns.find((column) => column.sortable);
  const [query, setQuery] = useState("");
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(initialPageSize);
  const [sortKey, setSortKey] = useState<string | null>(
    firstSortable?.key ?? null,
  );
  const [sortDirection, setSortDirection] = useState<SortDirection>("asc");

  const filteredRows = useMemo(() => {
    const normalizedQuery = query.trim().toLowerCase();

    if (!normalizedQuery) {
      return rows;
    }

    return rows.filter((row) =>
      row.searchText.toLowerCase().includes(normalizedQuery),
    );
  }, [query, rows]);

  const sortedRows = useMemo(() => {
    const activeColumn = columns.find((column) => column.key === sortKey);

    if (!activeColumn?.sortValue) {
      return filteredRows;
    }

    return [...filteredRows].sort((first, second) => {
      const firstValue = activeColumn.sortValue?.(first) ?? "";
      const secondValue = activeColumn.sortValue?.(second) ?? "";
      const comparison =
        typeof firstValue === "number" && typeof secondValue === "number"
          ? firstValue - secondValue
          : String(firstValue).localeCompare(String(secondValue));

      return sortDirection === "asc" ? comparison : -comparison;
    });
  }, [columns, filteredRows, sortDirection, sortKey]);

  const totalPages = Math.max(1, Math.ceil(sortedRows.length / pageSize));
  const visiblePage = Math.min(page, totalPages);
  const pageStart = (visiblePage - 1) * pageSize;
  const visibleRows = sortedRows.slice(pageStart, pageStart + pageSize);

  function handleSort(column: DataTableColumn<T>) {
    if (!column.sortable) {
      return;
    }

    if (sortKey === column.key) {
      setSortDirection((current) => (current === "asc" ? "desc" : "asc"));
    } else {
      setSortKey(column.key);
      setSortDirection("asc");
    }

    setPage(1);
  }

  function renderSortIcon(column: DataTableColumn<T>) {
    if (!column.sortable) {
      return null;
    }

    if (sortKey !== column.key) {
      return <ArrowUpDown className="h-3.5 w-3.5 text-zinc-400" />;
    }

    return sortDirection === "asc" ? (
      <ArrowUp className="h-3.5 w-3.5 text-brand-black" />
    ) : (
      <ArrowDown className="h-3.5 w-3.5 text-brand-black" />
    );
  }

  return (
    <section className="app-card overflow-hidden">
      <div className="flex flex-col gap-4 border-b border-zinc-200 p-5 md:flex-row md:items-center md:justify-between">
        <div>
          <h2 className="text-base font-semibold text-brand-black">{title}</h2>
          <p className="mt-1 text-sm text-zinc-500">
            {filteredRows.length} records
          </p>
        </div>
        <label className="relative block w-full md:max-w-sm">
          <Search className="pointer-events-none absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-zinc-400" />
          <input
            className="field pl-9"
            value={query}
            onChange={(event) => {
              setQuery(event.target.value);
              setPage(1);
            }}
            placeholder={searchPlaceholder}
            type="search"
          />
        </label>
      </div>

      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-zinc-200 text-left text-sm">
          <thead className="bg-zinc-50">
            <tr>
              {columns.map((column) => (
                <th
                  key={column.key}
                  className={cx(
                    "whitespace-nowrap px-5 py-3 text-xs font-semibold uppercase tracking-wide text-zinc-500",
                    column.className,
                  )}
                >
                  {column.sortable ? (
                    <button
                      type="button"
                      className="inline-flex items-center gap-2 text-left transition hover:text-brand-black"
                      onClick={() => handleSort(column)}
                    >
                      {column.header}
                      {renderSortIcon(column)}
                    </button>
                  ) : (
                    column.header
                  )}
                </th>
              ))}
            </tr>
          </thead>
          <tbody className="divide-y divide-zinc-100 bg-white">
            {visibleRows.map((row) => (
              <tr key={row.id} className="transition hover:bg-zinc-50">
                {columns.map((column) => (
                  <td
                    key={`${row.id}-${column.key}`}
                    className={cx("whitespace-nowrap px-5 py-4", column.className)}
                  >
                    {column.accessor(row)}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <div className="flex flex-col gap-3 border-t border-zinc-200 p-4 sm:flex-row sm:items-center sm:justify-between">
        <div className="flex items-center gap-2 text-sm text-zinc-500">
          <span>Rows</span>
          <select
            className="h-9 rounded-lg border border-zinc-200 bg-white px-2 text-sm text-brand-black focus:border-brand-gold focus:outline-none focus:ring-2 focus:ring-brand-gold/30"
            value={pageSize}
            onChange={(event) => {
              setPageSize(Number(event.target.value));
              setPage(1);
            }}
          >
            {[4, 6, 8, 10].map((size) => (
              <option key={size} value={size}>
                {size}
              </option>
            ))}
          </select>
        </div>

        <div className="flex items-center justify-between gap-3 sm:justify-end">
          <p className="text-sm text-zinc-500">
            Page {visiblePage} of {totalPages}
          </p>
          <div className="flex items-center gap-2">
            <button
              type="button"
              className="icon-button h-9 w-9 rounded-lg"
              disabled={visiblePage === 1}
              onClick={() => setPage((current) => Math.max(1, current - 1))}
            >
              <ChevronLeft className="h-4 w-4" />
            </button>
            <button
              type="button"
              className="icon-button h-9 w-9 rounded-lg"
              disabled={visiblePage === totalPages}
              onClick={() =>
                setPage((current) => Math.min(totalPages, current + 1))
              }
            >
              <ChevronRight className="h-4 w-4" />
            </button>
          </div>
        </div>
      </div>
    </section>
  );
}
