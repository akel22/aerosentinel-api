export function cx(...classes: Array<string | false | null | undefined>) {
  return classes.filter(Boolean).join(" ");
}

export function compactNumber(value: number) {
  return new Intl.NumberFormat("en-US", {
    notation: value >= 10000 ? "compact" : "standard",
    maximumFractionDigits: 1,
  }).format(value);
}

export function formatPercent(value: number) {
  return `${Math.round(value)}%`;
}

export function formatDateTime(value: string) {
  const date = new Date(value);

  if (Number.isNaN(date.getTime())) {
    return value;
  }

  return new Intl.DateTimeFormat("en-US", {
    month: "short",
    day: "numeric",
    hour: "2-digit",
    minute: "2-digit",
  }).format(date);
}

export function formatCoordinates(latitude: number, longitude: number) {
  const latDirection = latitude >= 0 ? "N" : "S";
  const lonDirection = longitude >= 0 ? "E" : "W";

  return `${Math.abs(latitude).toFixed(3)} ${latDirection}, ${Math.abs(
    longitude,
  ).toFixed(3)} ${lonDirection}`;
}

export function minutesAgo(minutes: number) {
  if (minutes < 1) {
    return "Just now";
  }

  return `${minutes} min ago`;
}
