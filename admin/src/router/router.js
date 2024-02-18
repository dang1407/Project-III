import { createRouter, createWebHistory } from "vue-router";
import Employee from "../views/employee/Employee.vue";
import Home from "@/views/home/Home.vue";
import ErrorPage from "../views/error/ErrorPage.vue";
import Login from "../views/login/Login.vue";
import Garage from "../views/garage/Garage.vue";
import ParkMember from "../views/garage/ParkMember.vue";
import ParkingHistoryChart from "../views/garage/ParkingHistoryChart.vue";
import DashBoard from "../views/dashboard/DashBoard.vue";
import Test from "../views/test/Test.vue";

import { useUserStore } from "@/stores/UserStore";

const routes = [
  {
    name: "Home",
    path: "/",
    component: Home,
  },
  {
    name: "Dash Board",
    path: "/dashboard",
    component: DashBoard,
  },
  {
    name: "Employee Page",
    path: "/employee",
    component: Employee,
  },
  {
    name: "Garage ",
    path: "/garage",
    component: Garage,
  },
  {
    name: "ParkMember",
    path: "/parkmember",
    component: ParkMember,
  },
  {
    name: "Parking History Chart",
    path: "/parkinghistory-chart",
    component: ParkingHistoryChart,
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

// router.beforeEach((to, from, next) => {
//   // we wanted to use the store here
//   const userStore = useUserStore();
//   if (userStore.isLogined) next();
//   else next("/login");
// });

// router.beforeEach((to) => {
//   // ✅ This will work because the router starts its navigation after
//   // the router is installed and pinia will be installed too
//   const store = useStore()

//   if (to.meta.requiresAuth && !store.isLoggedIn) return '/login'
// })

export default router;
