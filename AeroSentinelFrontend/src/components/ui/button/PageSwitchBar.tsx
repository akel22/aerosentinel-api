interface PageSwitchBarProps {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

export default function PageSwitchBar({
  currentPage = 1,
  totalPages = 1,
  onPageChange,
}: PageSwitchBarProps) {

  const goToPreviousPage = () => {
    if (currentPage > 1) {
      onPageChange(currentPage - 1);
    }
  };

  const goToNextPage = () => {
    if (currentPage < totalPages) {
      onPageChange(currentPage + 1);
    }
  };

  const getPageNumbers = () => {
    const pages: (number | string)[] = [];

    if (totalPages <= 7) {
      for (let i = 1; i <= totalPages; i++) {
        pages.push(i);
      }
      return pages;
    }

    if (currentPage <= 4) {
      pages.push(1, 2, 3, 4, 5, "...", totalPages);
      return pages;
    }

    if (currentPage >= totalPages - 3) {
      pages.push(
        1,
        "...",
        totalPages - 4,
        totalPages - 3,
        totalPages - 2,
        totalPages - 1,
        totalPages
      );
      return pages;
    }

    pages.push(
      1,
      "...",
      currentPage - 1,
      currentPage,
      currentPage + 1,
      "...",
      totalPages
    );

    return pages;
  };

  return (
    <nav
      aria-label="Pagination"
      className="flex justify-center space-x-2 mt-8"
    >
      {/* Previous */}
      <button
        type="button"
        onClick={goToPreviousPage}
        disabled={currentPage === 1}
        aria-label="Previous page"
        className="flex items-center justify-center shrink-0 bg-gray-100 w-9 h-9 rounded-md
          disabled:cursor-default disabled:opacity-50
          hover:bg-gray-200
          focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500
          dark:bg-neutral-800 dark:hover:bg-neutral-700"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="fill-slate-600 size-3 rotate-180 dark:fill-slate-50"
          viewBox="0 0 451.846 451.847"
          aria-hidden="true"
        >
          <path d="M345.441 248.292 151.154 442.573c-12.359 12.365-32.397 12.365-44.75 0-12.354-12.354-12.354-32.391 0-44.744L278.318 225.92 106.409 54.017c-12.354-12.359-12.354-32.394 0-44.748 12.354-12.359 32.391-12.359 44.75 0l194.287 194.284c6.177 6.18 9.262 14.271 9.262 22.366 0 8.099-3.091 16.196-9.267 22.373" />
        </svg>
      </button>

      {/* Page Numbers */}
      {getPageNumbers().map((page, index) => {
        if (page === "...") {
          return (
            <span
              key={`ellipsis-${index}`}
              className="flex items-center justify-center shrink-0 text-sm font-semibold text-slate-900 w-9 h-9 rounded-md dark:text-slate-50"
            >
              ...
            </span>
          );
        }

        const pageNumber = Number(page);
        const isActive = pageNumber === currentPage;

        return (
          <button
            key={`page-${pageNumber}`}
            type="button"
            onClick={() => onPageChange(pageNumber)}
            aria-current={isActive ? "page" : undefined}
            className={`flex items-center justify-center shrink-0 text-sm font-semibold w-9 h-9 rounded-md
              focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500
              ${
                isActive
                  ? "bg-blue-900 text-white"
                  : "text-slate-900 hover:bg-gray-100 dark:bg-neutral-800 dark:text-slate-50 dark:hover:bg-neutral-700"
              }`}
          >
            {pageNumber}
          </button>
        );
      })}

      {/* Next */}
      <button
        type="button"
        onClick={goToNextPage}
        disabled={currentPage === totalPages}
        aria-label="Next page"
        className="flex items-center justify-center shrink-0 bg-gray-200 w-9 h-9 rounded-md
          disabled:cursor-default disabled:opacity-50
          hover:bg-gray-100
          focus:outline-none focus-visible:ring-2 focus-visible:ring-blue-500
          dark:bg-neutral-800 dark:text-slate-50 dark:hover:bg-neutral-700"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="fill-slate-600 size-3 dark:fill-slate-50"
          viewBox="0 0 451.846 451.847"
          aria-hidden="true"
        >
          <path d="M345.441 248.292 151.154 442.573c-12.359 12.365-32.397 12.365-44.75 0-12.354-12.354-12.354-32.391 0-44.744L278.318 225.92 106.409 54.017c-12.354-12.359-12.354-32.394 0-44.748 12.354-12.359 32.391-12.359 44.75 0l194.287 194.284c6.177 6.18 9.262 14.271 9.262 22.366 0 8.099-3.091 16.196-9.267 22.373" />
        </svg>
      </button>
    </nav>
  );
}