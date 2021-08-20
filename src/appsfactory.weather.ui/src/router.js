import { createWebHistory, createRouter } from "vue-router";
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
    name: "weather",
    component: Weather,
  },
  {
    path: "/login",
    name:"login",
    component: Login,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const publicPages = [];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem("user");
  if (authRequired && !loggedIn) {
    next({
        name: "login"
     })
  } else {
    next();
  }
});

export default router;
