import { createRouter, createWebHistory } from "vue-router";
import Employee from "../views/employee/Employee.vue";
import Home from "@/views/home/Home.vue";
import ErrorPage from "../views/error/ErrorPage.vue";
import Login from "../views/login/Login.vue";
import Test from "../views/test/Test.vue";
const routes = [
  {
    name: "Home",
    path: "/",
    component: Home,
  },
  {
    name: "Employee Page",
    path: "/employee",
    component: Employee,
  },
  {
    name: "Login Page",
    path: "/login",
    component: Login,
  },
  {
    name: "Test",
    path: "/test",
    component: Test,
  },
  {
    path: "/:pathMatch(.*)*",
    component: ErrorPage,
    name: "Page not found",
  },
];

const router = createRouter({
  // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
  history: createWebHistory(),
  routes, // short for `routes: routes`
});

export default router;
