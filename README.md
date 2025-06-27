# The Magic Den

## Contents

1. [Project description](#1-project-description)

2. [Technologies](#2-technologies)

3. [App Overview](#3-app-overview)

## 1. Project description

The Magic Den is an online store for board games and related products developed with ASP.Net Core.

### Main functionalities

-   Ability to view every product for all users, including non-registered users.

-   Ability to filter products by multiple criteria.

-   Ability to sort games by price.

-   Registered users have shopping carts and can make orders.

-   When making an order, registered users can buy a discount for their orders by utilizing the special currency "Magic Points" which can be accumulated by making orders.

-   Administrators can manipulate games, categories, subcategories and brands.

-   Administrators can also see a history of orders and delete users.

-   Administrator has access to a simple statistical menu

## 2. Technologies

-   The backend is written in **C#** and **ASP.Net Core** with the **MVC** pattern.

-   User management wiht **ASP.Net Identity**.

-   Database - **SQL Server**.

-   The app uses **three-layer architecture**.

-   Followed design patterns - combination of **General Repository** and **Unit of work**. **Factory** for the use of migrations during development.

-   The frontend is built with: **HTML**, **CSS**, **JavaScript**, **Bootstrap** and **Razor**.

## 3. App Overview

Home page
![home page](/docs_images/home_page.png)

Main menu
![main menu](/docs_images/main_menu.png)

Games list
![games list](/docs_images/games_list.png)

Game details
![game details](/docs_images/game_details.png)

Filter menu
![filter menu](/docs_images/filter_menu.png)

Shopping cart
![shopping cart](/docs_images/shopping_cart.png)

Checkout
![checkout](/docs_images/checkout.png)

Contacts page
![contacts page](/docs_images/contacts_page.png)

Admin dashboard
![admin dashboard](/docs_images/admin_dashboard.png)

Admin games list
![admin games list](/docs_images/admin_games_list.png)

Admin game details
![admin details](/docs_images/admin_details.png)

Create game
![create game](/docs_images/create_game.png)

Admin brands list
![all brands](/docs_images/all_brands.png)

Admin categories and subcategories
![all categories](/docs_images/all_categories.png)
