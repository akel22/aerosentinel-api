type NavItem = {
  label: string
  active?: boolean
}

type SidebarProps = {
  collapsed?: boolean
}

const navItems: NavItem[] = [
  { label: 'Dashboard', active: true },
  { label: 'Aircraft' },
  { label: 'Telemetry' },
  { label: 'Security' },
  { label: 'System' },
  { label: 'Settings' },
]

export function Sidebar({ collapsed = false }: SidebarProps) {
  return (
    <aside
      className={[
        'flex flex-col border-r border-slate-700 bg-[#070f1a] text-slate-200 transition-all duration-200',
        collapsed ? 'hidden w-0 overflow-hidden border-r-0 lg:flex lg:w-[88px]' : 'w-full max-w-[240px] lg:min-h-screen',
      ].join(' ')}
    >
      <div className="border-b border-slate-700/80 px-5 py-5">
        <div className={['flex items-center gap-3', collapsed ? 'justify-center' : ''].join(' ')}>
          <div className="flex h-9 w-9 items-center justify-center rounded-sm border border-slate-600 bg-slate-900 text-sm font-semibold tracking-[0.14em] text-slate-200">
            P
          </div>
          {!collapsed ? (
            <div>
              <div className="text-lg font-semibold tracking-tight text-slate-50">PRIME-Air</div>
              <div className="text-[10px] uppercase tracking-[0.18em] text-slate-400">
                Telemetry Security Platform
              </div>
            </div>
          ) : null}
        </div>
      </div>

      <nav className="flex-1 space-y-1 px-3 py-4">
        {navItems.map((item) => (
          <button
            key={item.label}
            type="button"
            className={[
              'flex w-full items-center justify-between rounded-sm px-3 py-2.5 text-left text-sm font-medium transition-colors',
              item.active
                ? 'border border-slate-600 bg-slate-800 text-slate-50'
                : 'text-slate-300 hover:bg-slate-900 hover:text-slate-100',
              collapsed ? 'justify-center px-2' : '',
            ].join(' ')}
          >
            {!collapsed ? <span>{item.label}</span> : <span className="text-[10px] uppercase tracking-[0.16em]">{item.label.slice(0, 2)}</span>}
          </button>
        ))}
      </nav>
    </aside>
  )
}
