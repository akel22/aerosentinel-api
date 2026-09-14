import type { StatusTone } from "../types";

export function toneFromStatus(status: string): StatusTone {
  const normalized = status.toLowerCase();

  if (
    normalized.includes("success") ||
    normalized.includes("verified") ||
    normalized.includes("ongoing") ||
    normalized.includes("active")
  ) {
    return "success";
  }

  if (
    normalized.includes("failed") ||
    normalized.includes("spoof") ||
    normalized.includes("reject") ||
    normalized.includes("restricted") ||
    normalized.includes("high")
  ) {
    return "danger";
  }

  if (
    normalized.includes("delayed") ||
    normalized.includes("pending") ||
    normalized.includes("watch") ||
    normalized.includes("medium")
  ) {
    return "warning";
  }

  if (normalized.includes("standby") || normalized.includes("low")) {
    return "gold";
  }

  return "neutral";
}
