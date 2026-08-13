# Software Requirements Specification

**Project:** HelpDesk IT Service Desk API
**Author:** Bahaaio

---

## 1. Introduction

The purpose of this document is to specify the functional and non-functional requirements for the HelpDesk IT Service Desk API. The system supports IT support operations, allowing employees to request assistance and technicians to manage and resolve support issues. The design now includes tickets, comments, attachments, tag-based categorization, and ticket upvote/downvote functionality.

---

## 2. Functional Requirements

### 2.1 User Account Management

- **FR-USER-001** The system shall allow users to register and login.
- **FR-USER-002** The system shall authenticate users and assign the role of Employee or Technician.
- **FR-USER-003** The system shall require authentication for all operations except registration and login.

### 2.2 Ticket Management

- **FR-TKT-001** The system shall allow authenticated Employees to create support tickets.
- **FR-TKT-002** The system shall persist each ticket and associate it with its creator.
- **FR-TKT-003** The system shall require each ticket to have a title, description, createdAt, updatedAt, status, and createdBy.
- **FR-TKT-004** The system shall support ticket status transitions: Open → Closed, with the ability to reopen Resolved tickets.
- **FR-TKT-005** The system shall allow Employees to view and comment on any ticket.
- **FR-TKT-006** The system shall prevent Employees from updating tickets they did not create.
- **FR-TKT-007** The system shall allow Technicians to view, update, comment, resolve, and close any ticket.
- **FR-TKT-008** The system shall provide filtering, sorting, and pagination for ticket queries.

### 2.3 Comment Management

- **FR-COM-001** The system shall allow authorized users to add comments to tickets they are permitted to view.
- **FR-COM-002** The system shall persist each comment with its author, content, associated ticket, and creation time.
- **FR-COM-003** The system shall allow all authorized users to view all comments on any ticket.

### 2.4 Attachment Management

- **FR-ATT-001** The system shall allow users to upload and associate multiple attachments with a ticket.
- **FR-ATT-002** The system shall store, for each attachment: file URL, upload time, uploader.

### 2.5 Tag Management

- **FR-TAG-001** The system shall allow Technicians to create, update, and delete tags.
- **FR-TAG-002** The system shall allow Technicians to view all available tags.
- **FR-TAG-003** The system shall allow tickets to have multiple tags assigned.

### 2.6 Ticket Voting

- **FR-VOTE-001** The system shall allow authenticated Employees to upvote or downvote any ticket.
- **FR-VOTE-002** The system shall ensure each Employee can vote (up or down) at most once per ticket.
- **FR-VOTE-003** The system shall allow Employees to change their vote on a ticket.
- **FR-VOTE-004** The system shall maintain the total vote count (upvotes minus downvotes) per ticket.

---

## 3. Non-Functional Requirements

### 3.1 Security & Authorization

- **NFR-SEC-001** The system shall use ASP.NET Core Identity for authentication.
- **NFR-SEC-002** The system shall use secure cookies for all authenticated API calls.
- **NFR-SEC-003** The system shall enforce role-based access control for protected operations.
- **NFR-SEC-004** The system shall enforce resource-based authorization for tickets, comments, attachments, and votes.

### 3.2 Usability & Reliability

- **NFR-REL-001** The system shall return descriptive error messages for invalid requests.
- **NFR-REL-002** The system shall provide machine-readable API documentation (e.g., OpenAPI).

---

## 4. Technical Constraints

- **TC-API-001** The system shall expose a RESTful HTTP API.
- **TC-DB-001** The system shall persist data in PostgreSQL.
- **TC-DPL-001** The system shall be deployable in a containerized environment.
- **TC-VAL-001** The system shall validate all incoming user data.

---
