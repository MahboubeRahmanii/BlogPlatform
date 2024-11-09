Blog Platform
A comprehensive blog platform built with .NET 8, designed to deliver a seamless and interactive user experience. This platform supports key blogging features, real-time notifications, and scheduled posts.

Features
Post Creation & Interaction: Users can create posts, rate other users' posts, and add comments, encouraging active engagement within the platform.
Real-Time Notifications: Utilizing SignalR, real-time notifications are delivered to post owners whenever a new comment or rating is added.
Scheduled Publishing: Users can schedule posts to be published at a future date and time, managed by a custom background service.

Technologies Used
.NET 8
SQLite for database management
Entity Framework Core for ORM
Minimal API structure for simplicity and efficiency
Carter for clean, modular routing
SignalR for real-time notifications
FluentValidation for input validation
BackgroundService for post scheduling and periodic checks