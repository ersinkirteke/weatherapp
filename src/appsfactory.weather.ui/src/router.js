import { createWebHistory, createRouter,isNavigationFailure, NavigationFailureType  } from "vue-router";
import Weather from "./components/Weather.vue";
import Login from "./components/Login.vue";

const routes = [
  {
    path: "/",
    name: "home",
    component: Weather,
  },
  {
    path: "/weather",
    name: "Weather",
    component: Weather,
  },
  {
    path: "/login",
    name:"Login",
    component: Login,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const publicPages = ['/login'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem("user");
  if (authRequired && !loggedIn) {
    next({
        name: "Login"
     })
  } else {
    next();
  }
});

export default router;
