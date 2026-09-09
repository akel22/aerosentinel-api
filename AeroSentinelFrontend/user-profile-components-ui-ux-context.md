# User Profile Page — React Component UI/UX Context

## Purpose

This document provides the **UI/UX and component-structure context** for the React components that make up the User Profile page.

The goal is to keep the five components visually and behaviorally consistent with the existing profile-page design while preserving a clean React component hierarchy.

All components use **TailwindCSS only** for styling. Do not introduce Bootstrap, CSS modules, styled-components, inline style objects, or another UI framework unless explicitly requested.

---

# 1. Component Hierarchy

The profile page is composed of these five React components:

```text
UserProfileCard.tsx
├── UserInfoCard.tsx
├── UserMetaCard.tsx
│   └── EditProfileModal.tsx   (shown only when its Edit button is clicked)
└── UserAddressCard.tsx
    └── EditProfileModal.tsx   (shown only when its Edit button is clicked)
```

### Components

1. **UserProfileCard.tsx**
   - Parent/container component for the entire profile content.
   - Establishes the main layout, spacing, width, background treatment, and visual grouping.
   - Renders the child profile sections.

2. **UserInfoCard.tsx**
   - Child of `UserProfileCard.tsx`.
   - Represents the profile-summary/header section.
   - Shows the user's avatar/profile image, name, role/job title, organization/department, and the primary "Edit Profile" action.

3. **UserMetaCard.tsx**
   - Child of `UserProfileCard.tsx`.
   - Represents the user's personal information.
   - Contains an `Edit` button for modifying this section.
   - Its edit interaction may render `EditProfileModal.tsx`.

4. **UserAddressCard.tsx**
   - Child of `UserProfileCard.tsx`.
   - Represents the user's address/contact-location information.
   - Contains its own `Edit` button.
   - Its edit interaction may render `EditProfileModal.tsx`.

5. **EditProfileModal.tsx**
   - Reusable modal used to edit profile information.
   - It is conditionally rendered only after an appropriate `Edit` action is triggered.
   - It may be controlled by either `UserMetaCard.tsx` or `UserAddressCard.tsx`.
   - The same component should be reusable rather than creating separate modal components for each card.

> Note: `EditProfileModal.tsx` is the fifth component in the list. The original component description referred to it as the "fourth one"; treat the filename/component relationship above as authoritative.

---

# 2. Existing Visual Design Context

The supplied screenshot represents the current visual direction of the profile page.

The page uses a **dark, modern admin-dashboard aesthetic** designed for an operational/enterprise application.

The profile content sits inside a dark page area with a centered content container. The visual hierarchy is built from:

- Dark navy/blue page backgrounds
- Slightly lighter dark cards
- Thin, low-contrast borders
- Rounded corners
- Small/light typography
- Bright blue accent actions
- Generous internal spacing
- Compact admin-dashboard controls
- Clear separation between information groups

The design should feel:

- Professional
- Technical
- Operational
- Minimal
- Clean
- Information-dense without feeling crowded

Avoid making the profile page look like a consumer/social-media profile.

---

# 3. Overall Page Layout

`UserProfileCard.tsx` is the main visual container.

The screenshot indicates a layout approximately like this:

```text
┌──────────────────────────────────────────────────────────────┐
│ Profile                                      Home > Profile │
│                                                              │
│ ┌──────────────────────────────────────────────────────────┐ │
│ │ Profile                                                  │ │
│ │                                                          │ │
│ │ ┌──────────────────────────────────────────────────────┐ │ │
│ │ │ [Avatar]  Admin                                      │ │ │
│ │ │           Operations Officer | Aircraft Control     │ │ │
│ │ │                                      [Edit Profile] │ │ │
│ │ └──────────────────────────────────────────────────────┘ │ │
│ │                                                          │ │
│ │ ┌──────────────────────────────────────────────────────┐ │ │
│ │ │ Personal Information                         [Edit]  │ │ │
│ │ │                                                      │ │ │
│ │ │ First Name                    Last Name              │ │ │
│ │ │ Musharof                      Chowdhury              │ │ │
│ │ │                                                      │ │ │
│ │ │ Email Address                 Phone                  │ │ │
│ │ │ randomuser@pimjo.com          +09 363 398 46        │ │ │
│ │ │                                                      │ │ │
│ │ │ Bio                                                  │ │ │
│ │ │ Team Manager                                         │ │ │
│ │ └──────────────────────────────────────────────────────┘ │ │
│ │                                                          │ │
│ │ ┌──────────────────────────────────────────────────────┐ │ │
│ │ │ Address                                      [Edit]  │ │ │
│ │ │                                                      │ │ │
│ │ │ Country                       City/State              │ │ │
│ │ │ United States                 Phoenix, Arizona...    │ │ │
│ │ │                                                      │ │ │
│ │ │ Postal Code                   Tax ID                  │ │ │
│ │ │ ERT 2489                      AS45658384             │ │ │
│ │ └──────────────────────────────────────────────────────┘ │ │
│ └──────────────────────────────────────────────────────────┘ │
└──────────────────────────────────────────────────────────────┘
```

The screenshot also includes the application's broader sidebar/top navigation. Those elements are outside the responsibility of these five profile components unless existing application layout code explicitly places them around `UserProfileCard.tsx`.

---

# 4. UserProfileCard.tsx

## Responsibility

`UserProfileCard.tsx` is the **parent composition component**.

It should primarily answer:

> "How are the profile sections arranged on the page?"

It should not contain large amounts of section-specific UI logic.

### Responsibilities

- Render the profile page's main content container.
- Maintain the vertical order of profile sections.
- Provide consistent spacing between child cards.
- Establish the overall card/background treatment.
- Pass the appropriate data and callbacks to child components.
- Coordinate modal state when the architecture requires the parent to own that state.

### Expected visual structure

```text
UserProfileCard
    ├── page/card title
    ├── UserInfoCard
    ├── UserMetaCard
    └── UserAddressCard
```

### Layout principles

Use TailwindCSS utilities for:

- `max-w-*` / width constraints
- responsive horizontal padding
- vertical spacing such as `space-y-*` or `gap-*`
- rounded outer container
- dark background
- subtle border
- responsive behavior

The parent should not over-style individual fields that belong to child components.

### Important architectural principle

Prefer **composition over duplication**.

`UserProfileCard.tsx` should provide structure and coordination, while the children own the UI details of their respective sections.

---

# 5. UserInfoCard.tsx

## Responsibility

This component represents the **profile identity/summary area** at the top of the profile content.

It is visually different from the information cards below it because it acts as a profile header.

### Information shown

Based on the screenshot, the section can contain:

- Profile/avatar image
- User name
- User role/title
- Organization or department
- Primary `Edit Profile` button

Example:

```text
[avatar]   Admin
           Operations Officer | Aircraft Control

                                      [Edit Profile]
```

### Visual hierarchy

The user's name should have stronger visual emphasis than the secondary metadata.

Suggested hierarchy:

```text
Name
  ↓
Role / organization
  ↓
Primary action
```

The avatar should be visually prominent enough to identify the profile but should not dominate the page.

### Edit Profile action

The large `Edit Profile` button shown in this header represents a **primary profile action**.

It should visually stand apart from the smaller section-level `Edit` buttons used inside the lower cards.

Use TailwindCSS to maintain:

- rounded button shape
- blue/primary accent
- icon + text alignment
- clear hover state
- visible focus state
- appropriate disabled state if necessary

---

# 6. UserMetaCard.tsx

## Responsibility

`UserMetaCard.tsx` represents the user's **personal information**.

The screenshot labels this section:

> Personal Information

### Information shown

Current visual content includes:

- First Name
- Last Name
- Email Address
- Phone
- Bio

The component should organize these fields into a readable responsive grid.

Example desktop layout:

```text
First Name                         Last Name
Musharof                           Chowdhury

Email Address                      Phone
randomuser@pimjo.com               +09 363 398 46

Bio
Team Manager
```

### Card header

The header should contain:

```text
Personal Information                         [Edit]
```

The `Edit` button is a **secondary/local action**, not the primary page-level action.

It should therefore be visually lighter and smaller than the large `Edit Profile` button in `UserInfoCard.tsx`.

### Responsive behavior

On larger screens, information can use a two-column grid.

On smaller screens, fields should collapse into a single readable column.

Conceptually:

```text
Desktop:
[Field]                 [Field]

[Field]                 [Field]

[Field...........................]

Mobile:
[Field]
[Field]
[Field]
[Field]
[Field]
```

Do not force the desktop two-column layout on narrow screens.

---

# 7. UserAddressCard.tsx

## Responsibility

`UserAddressCard.tsx` represents the user's **address/location-related information**.

### Information shown

The screenshot currently includes:

- Country
- City/State
- Postal Code
- Tax ID

### Card header

The structure should be:

```text
Address                                      [Edit]
```

The `Edit` button should follow the same visual language as the `Edit` button inside `UserMetaCard.tsx`.

Consistency between the two cards is important.

### Layout

Desktop:

```text
Country                              City/State

United States                        Phoenix, Arizona, United States


Postal Code                          Tax ID

ERT 2489                             AS45658384
```

Mobile:

```text
Country
United States

City/State
Phoenix, Arizona, United States

Postal Code
ERT 2489

Tax ID
AS45658384
```

---

# 8. EditProfileModal.tsx

## Responsibility

`EditProfileModal.tsx` is a **conditional editing interface**, not a permanently visible card.

It should only appear after an appropriate edit button has been clicked.

### Important behavior

There are two cards that can trigger editing:

```text
UserMetaCard
    └── Edit → EditProfileModal

UserAddressCard
    └── Edit → EditProfileModal
```

The modal should **not** be visible during the normal profile-page state.

Initial state:

```text
Modal closed
```

After clicking the `Edit` button:

```text
Modal opened
```

After cancel/save/close:

```text
Modal closed
```

### Conditional rendering

Conceptually:

```tsx
{isModalOpen && (
  <EditProfileModal
    ...
  />
)}
```

The exact state ownership may vary depending on the implementation architecture.

---

# 9. Modal Reusability

`EditProfileModal.tsx` should ideally be reusable for different profile sections.

Avoid making the component tightly coupled to only one card.

For example, the parent can conceptually communicate which section is being edited:

```tsx
<EditProfileModal
  section="personal"
/>
```

or:

```tsx
<EditProfileModal
  section="address"
/>
```

The exact prop names are implementation details, but the principle is important:

> One reusable modal component should support the relevant profile-editing contexts instead of creating duplicated modal implementations.

If the application design requires different fields for the two sections, the modal can render the appropriate form content based on its current editing context.

---

# 10. Modal UX Requirements

The modal should follow the same dark enterprise-dashboard visual language as the page.

### Visual characteristics

- Dark surface
- Slightly elevated appearance from the page background
- Rounded corners
- Subtle border
- Clear heading
- Clear form labels
- Strong focus states
- Primary save/update action
- Secondary cancel/close action
- Sufficient spacing between fields
- Responsive width

### Overlay

When the modal is open, the page behind it should visually recede through a semi-transparent overlay.

The overlay should:

- cover the viewport
- prevent accidental interaction with the background content
- preserve the modal's visual focus

### Interaction

A polished implementation should support:

- Close/cancel button
- Clicking outside the modal when appropriate
- `Escape` to close when appropriate
- keyboard focus behavior
- visible focus rings
- disabled/loading state during save operations

Do not sacrifice accessibility for visual styling.

---

# 11. TailwindCSS Design Language

All five components should use TailwindCSS utilities consistently.

## Color direction

The screenshot suggests a palette centered around:

- Very dark navy backgrounds
- Dark blue-gray cards
- Subtle blue-gray borders
- White/light-gray primary text
- Muted blue-gray secondary text
- Bright blue primary actions

Do not introduce many unrelated colors.

Use a restrained palette so the blue accent communicates interaction and importance.

## Borders

Use subtle borders instead of heavy outlines.

Conceptually:

```text
Card:
dark background
+ low-contrast border
+ rounded corners
```

The border should separate surfaces without becoming the dominant visual element.

## Radius

Use rounded corners consistently across:

- Cards
- Buttons
- Inputs
- Modal
- Avatar container where appropriate

Avoid mixing many unrelated radius values.

## Spacing

The screenshot uses generous spacing between:

- sections
- section headers
- labels
- values
- card boundaries

Do not compress the information merely to fit more content.

---

# 12. Typography Hierarchy

The UI uses a compact dashboard typography hierarchy.

Recommended conceptual hierarchy:

```text
Page title
    ↓
Card title
    ↓
User name
    ↓
Field label
    ↓
Field value
    ↓
Secondary metadata
```

### Labels

Field labels such as:

```text
FIRST NAME
EMAIL ADDRESS
COUNTRY
POSTAL CODE
```

should be visually quieter than their values.

They may use:

- smaller text size
- uppercase styling
- increased letter spacing
- muted color

### Values

Values should be more visually prominent and readable.

Do not make every piece of text bold.

---

# 13. Buttons

There are two distinct button levels.

## Primary action

Used by `UserInfoCard.tsx`:

```text
Edit Profile
```

This should be visually prominent.

## Section edit action

Used by:

- `UserMetaCard.tsx`
- `UserAddressCard.tsx`

These buttons should be smaller and visually secondary:

```text
Edit
```

Both should share the same design language while clearly communicating their different importance levels.

---

# 14. State and Interaction Model

The key interaction is:

```text
Profile loaded
      ↓
User clicks "Edit"
      ↓
EditProfileModal opens
      ↓
User edits fields
      ↓
Save OR Cancel
      ↓
Modal closes
      ↓
Profile display updates if save succeeded
```

The modal should not permanently occupy layout space when closed.

Prefer conditional rendering or an equivalent controlled-modal approach rather than visually hiding a permanently mounted card without reason.

---

# 15. Data Flow

The component hierarchy should keep data flow predictable.

Conceptually:

```text
Parent
  │
  ├── user profile data
  │
  ├── UserInfoCard
  │
  ├── UserMetaCard
  │       │
  │       └── edit event
  │
  └── UserAddressCard
          │
          └── edit event
```

If modal state is lifted to `UserProfileCard.tsx`, the flow may instead look like:

```text
UserProfileCard
      │
      ├── isModalOpen
      ├── editingSection
      └── open/close handlers
             │
             ├── UserMetaCard
             ├── UserAddressCard
             │
             └── EditProfileModal
```

Either approach is acceptable as long as the state ownership is clear and the implementation avoids unnecessary duplication.

---

# 16. Separation of Responsibilities

Keep responsibilities clearly separated.

### UserProfileCard.tsx

Owns:

- page composition
- overall layout
- section ordering
- shared coordination/state when appropriate

Does not own:

- detailed field rendering
- detailed modal form markup

### UserInfoCard.tsx

Owns:

- avatar
- name
- role/organization metadata
- primary profile action

### UserMetaCard.tsx

Owns:

- personal-information display
- personal-information edit trigger

### UserAddressCard.tsx

Owns:

- address display
- address edit trigger

### EditProfileModal.tsx

Owns:

- modal shell
- modal form UI
- form interaction
- save/cancel actions
- relevant editing context

---

# 17. Responsive Design

The original screenshot is desktop-oriented, but the components should remain responsive.

### Desktop

Use the available horizontal space efficiently:

```text
Label/value pairs can appear in 2 columns
```

### Tablet

Reduce gaps and allow content to wrap naturally.

### Mobile

Stack fields vertically:

```text
Field
Value

Field
Value
```

Buttons should remain large enough to comfortably interact with.

Do not rely on fixed widths that cause horizontal scrolling.

---

# 18. Accessibility Expectations

The UI should be visually polished while remaining accessible.

Important considerations:

- Use semantic `button` elements for actions.
- Do not use clickable `div` elements when a button is appropriate.
- Provide meaningful labels for icon-only buttons.
- Ensure keyboard focus is visible.
- Modal should have an accessible name/title.
- Form inputs should have associated labels.
- Disabled/loading states should be distinguishable.
- Maintain sufficient text/background contrast.
- Avoid communicating state using color alone.

---

# 19. Implementation Guidance for the Components

When implementing or modifying these components:

1. Preserve the existing visual language shown in the screenshot.
2. Use TailwindCSS utilities only.
3. Keep each component responsible for one clear UI concern.
4. Reuse the same card/button/input design patterns across sections.
5. Do not duplicate modal implementations.
6. Keep edit modals hidden until the relevant edit action occurs.
7. Prefer responsive Tailwind layouts such as `grid`, `grid-cols-*`, `md:grid-cols-*`, `gap-*`, and responsive padding/width utilities.
8. Avoid excessive absolute positioning because the profile content should remain responsive.
9. Preserve the spacing and visual hierarchy of the existing design.
10. When changing one component, avoid unexpectedly changing the appearance of unrelated profile sections.

---

# 20. Desired Component Relationship

The intended mental model for the implementation is:

```text
                  ┌─────────────────────────┐
                  │   UserProfileCard.tsx   │
                  │                         │
                  │  Main profile container │
                  └────────────┬────────────┘
                               │
             ┌─────────────────┼─────────────────┐
             │                 │                 │
             ▼                 ▼                 ▼
      ┌──────────────┐  ┌──────────────┐  ┌──────────────┐
      │ UserInfoCard │  │ UserMetaCard │  │UserAddressCard│
      │              │  │              │  │              │
      │ Profile      │  │ Personal     │  │ Address      │
      │ summary      │  │ information  │  │ information  │
      └──────────────┘  └──────┬───────┘  └──────┬───────┘
                               │                 │
                               │ Edit            │ Edit
                               ▼                 ▼
                         ┌────────────────────────────┐
                         │    EditProfileModal.tsx    │
                         │                            │
                         │  Conditional edit modal   │
                         └────────────────────────────┘
```

---

# 21. Key UX Principle

The profile page should communicate information in **layers**:

### Layer 1 — Who is this user?

`UserInfoCard.tsx`

### Layer 2 — What are the user's personal details?

`UserMetaCard.tsx`

### Layer 3 — Where is the user's registered address information?

`UserAddressCard.tsx`

### Layer 4 — How can the user modify this information?

`EditProfileModal.tsx`

This structure keeps the page easy to scan while making editing available exactly where users expect it.

---

# 22. Important Constraint

Do not redesign the entire application based solely on this document.

This context is specifically intended to guide the implementation of the five profile-related React components while preserving the existing application's visual language.

When actual component source code is later provided, use this document together with that code to determine:

- existing props
- state ownership
- event handlers
- data structures
- API integration points
- current Tailwind classes
- existing reusable UI primitives

Do not unnecessarily rewrite working application logic merely to match the visual description.

---

# 23. Screenshot Reference

The supplied screenshot should be treated as the primary visual reference for:

- overall profile-card proportions
- spacing
- dark theme
- card hierarchy
- button hierarchy
- typography scale
- two-column information layout
- section separation
- alignment

The screenshot shows the intended **current design**, not necessarily the final implementation.

Future changes should improve or preserve this visual system rather than introduce an unrelated design language.
