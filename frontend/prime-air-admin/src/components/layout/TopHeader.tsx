type TopHeaderProps = {
  onToggleSidebar: () => void
}

export function TopHeader({ onToggleSidebar }: TopHeaderProps) {
  return (
    <header className="flex items-center justify-between gap-4 border-b border-slate-700 bg-[#07121d] px-4 py-4 md:px-6">
      <div className="flex items-center gap-3">
        <button
          type="button"
          onClick={onToggleSidebar}
          className="inline-flex h-10 items-center justify-center rounded-md border border-slate-700 bg-slate-900 px-3 text-xs font-medium uppercase tracking-[0.18em] text-slate-200 lg:hidden"
          aria-label="Toggle sidebar"
        >
          Menu
        </button>

        <div>
          <p className="text-[10px] font-medium uppercase tracking-[0.18em] text-slate-500">PRIME-Air</p>
          <h1 className="mt-1 text-2xl font-semibold tracking-tight text-slate-50">Administrator</h1>
          <p className="mt-1 text-[10px] uppercase tracking-[0.18em] text-slate-500">
            This is currently mock UI only.
          </p>
        </div>
      </div>

     
    </header>
  )
}
